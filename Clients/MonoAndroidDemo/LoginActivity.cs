using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MonoAndroidDemo.Services;
using MonoAndroidDemo.Models.Ticket;
using MonoAndroidDemo.Models.Requests;


namespace MonoAndroidDemo
{
    [Activity(Label = "Login:", MainLauncher = true)]

    public class LoginActivity : Activity
    {
        EditText textUsername;
        EditText textPassword;
        Android.Widget.Button buttonLogin;
        TextView textLoginInfo;
        TextView textLoginInfosubtext;

        public static string username { get; set; }
        public static string userpass { get; set; }


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.LoginActivityLayout);

            textUsername = this.FindViewById<EditText>(Resource.Id.textUsername);
            textPassword = this.FindViewById<EditText>(Resource.Id.textPassword);
            buttonLogin = this.FindViewById<Android.Widget.Button>(Resource.Id.buttonLogin);
            textLoginInfo = this.FindViewById<TextView>(Resource.Id.textLoginInfo);
            textLoginInfosubtext = this.FindViewById<TextView>(Resource.Id.textLoginInfosubtext);

            this.Title = "User Login";



            buttonLogin.Click += async (sender, e) =>
            {
                var user = string.Empty;
                var password = string.Empty;

                this.RunOnUiThread(() => { user = this.textUsername.Text; });
                this.RunOnUiThread(() => { password = this.textPassword.Text; });

                username = user;
                userpass = password;


                //AzureService service = new AzureService();
                var api = new ZendeskApi("https://greenworkstools1552039260.zendesk.com/api/v2", user, password,"");

                this.textLoginInfo.Text = "Connecting to Zendesk...";

                IList<TicketField> allfields = new List<TicketField>();

                try
                {
                    //retrieve ticket fields from Zendesk
                    GroupTicketFieldResponse fields =  await api.Tickets.GetTicketFieldsAsync();
                    foreach (TicketField fielddata in fields.TicketFields)
                    {
                        allfields.Add(fielddata);
                    }
                    if (!String.IsNullOrEmpty(allfields[2].Title))
                    {
                        this.textLoginInfo.Text = "Succesfully connected and validated user!";
                        await Task.Delay(1000);
                        StartActivity(typeof(DecoderActivity));
                    }
                    else
                    {
                        this.textLoginInfo.Text = "Unable to retrieve ticket fields!";
                    }
                    
                }
                catch (WebException ex)
                {
                    this.textLoginInfo.Text = "Invalid Username or Password";
                    this.textLoginInfosubtext.Text = ex.Message;
                    await Task.Delay(2000);
                    this.textLoginInfo.Text = "Enter a valid Username and Password";
                    this.textLoginInfosubtext.Text = "";
                    //throw new WebException(ex.Message + " " + ex.Response.Headers.ToString(), ex);
                }

                /*if (this.textUsername.Text == service.textUsername)
                {
                    if (this.textPassword.Text == service.textPassword)
                    {
                        this.textLoginInfo.Text = "User Found!";

                        currentUser.Name = service.textName;
                        currentUser.Date = DateTime.Now;

                        this.textLoginInfo.Text = "Succesfully Logged in!";
                        StartActivity(typeof(DecoderActivity));
                    }
                    else
                    {
                        this.textLoginInfo.Text = "Invalid Password";
                    }
                }
                else
                {
                    this.textLoginInfo.Text = "Unrecognized Username";
                }*/
            };

        }
    }

}