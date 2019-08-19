using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using MonoAndroidDemo.Models.Ticket;
using MonoAndroidDemo.Models.Constants;
using MonoAndroidDemo.Services;


namespace MonoAndroidDemo
{
    [Activity(Label = "UnitInfoActivity")]
    public class UnitInfoActivity : Activity
    {
      
        /*TextView textDetails;
        TextView textWarranty;
        Spinner ddTypeOfWarranty;
        TextView textProduct;
        Spinner ddTypeOfProduct;
        TextView textFault;
        Spinner ddAreaOfFault;
        Button buttonUpload; */

        public CustomField warrantyselected = new CustomField();
        public CustomField productselected = new CustomField();
        public CustomField faultselected = new CustomField();

        IList<CustomField> customfields = new List<CustomField>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.SetContentView(Resource.Layout.UnitInfoLayout);

            TextView textDetails = this.FindViewById<TextView>(Resource.Id.textDetails);
            TextView textWarranty = this.FindViewById<TextView>(Resource.Id.textWarranty);
            Spinner ddTypeOfWarranty = this.FindViewById<Spinner>(Resource.Id.ddTypeOfWarranty);
            TextView textProduct = this.FindViewById<TextView>(Resource.Id.textProduct);
            Spinner ddTypeOfProduct = this.FindViewById<Spinner>(Resource.Id.ddTypeofProduct);
            TextView textFault = this.FindViewById<TextView>(Resource.Id.textFault);
            Spinner ddAreaOfFault = this.FindViewById<Spinner>(Resource.Id.ddAreaOfFault);
            Button buttonUpload = this.FindViewById<Button>(Resource.Id.buttonUpload);
            TextView textUploadStatus = this.FindViewById<TextView>(Resource.Id.textUploadStatus);

            //ids for custom fields
            warrantyselected.Id = 360019766751;
            productselected.Id = 360019468991;
            faultselected.Id = 360019766771;

            var uploadapi = new ZendeskApi("https://greenworkstools1552039260.zendesk.com/api/v2", LoginActivity.username, LoginActivity.userpass,"");


            ddTypeOfWarranty.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(warranty_ItemSelected);
            var warrantyadapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.warranty_array, Android.Resource.Layout.SimpleSpinnerItem);

            warrantyadapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            ddTypeOfWarranty.Adapter = warrantyadapter;

            ddTypeOfProduct.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(product_ItemSelected);
            var productadapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.product_array, Android.Resource.Layout.SimpleSpinnerItem);

            productadapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            ddTypeOfProduct.Adapter = productadapter;

            ddAreaOfFault.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(fault_ItemSelected);
            var faultadapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.fault_array, Android.Resource.Layout.SimpleSpinnerItem);

            faultadapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            ddAreaOfFault.Adapter = faultadapter;

            buttonUpload.Click += async (sender, e) =>
            {
                customfields.Add(warrantyselected);
                customfields.Add(productselected);
                customfields.Add(faultselected);
                var ticket = new Ticket()
                {
                    Subject = "this is a test ticket",
                    Priority = TicketPriorities.Normal,
                    TicketFormId = 360000437791,
                    Comment = new Comment()
                    {
                        Body = "This is a test ticket",
                        //set Public to true to receive an email notification to the requester's email
                        Public = true,
                        /*Uploads = new List<string>() { attachment.Token } //Add the attachment token here */
                    },
                    Requester = new Requester() { Email = "Vincent.James@cai.io" },
                    CustomFields = customfields
                };
                 var res = await uploadapi.Tickets.CreateTicketAsync(ticket);
                if (res.Ticket.Id != null)
                {
                    long? zenticketId = res.Ticket.Id;
                    textUploadStatus.Text = "Successfully uploaded ticket #" + zenticketId.ToString();
                }
                else
                {
                    textUploadStatus.Text = "Something went wrong";
                }
                

            };
        }
        private void warranty_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner warrantyspinner = (Spinner)sender;
            warrantyselected.Value = warrantyspinner.GetItemAtPosition(e.Position).ToString();
            if (warrantyselected.Value.Equals("Unit is not working"))
            {
                warrantyselected.Value = "replace_product__unit_is_marked_as__replace__in_the_replace/repair_list_";
            }
            else
            {
                warrantyselected.Value = "something_is_missing_in_the_box_at_delivery__only_new_";
            }
        }
        private void product_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner productspinner = (Spinner)sender;
            productselected.Value = productspinner.GetItemAtPosition(e.Position).ToString().ToLower();
            
        }
        private void fault_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner faultspinner = (Spinner)sender;
            faultselected.Value = faultspinner.GetItemAtPosition(e.Position).ToString().ToLower();
        }
    }
}
