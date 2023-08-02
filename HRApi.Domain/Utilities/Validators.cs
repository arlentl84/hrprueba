using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HRApi.Domain.Utilities
{
    public class Validators
    {
        private static Validators instance;

        public static Validators Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Validators();
                }

                return instance;
            }
        }


        public bool IsValidEmail(string email)
        {
            var valid = true;

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }
    }
}
