using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Data;
using System.IO;
using MemberSignInSystem.ModernUI.ViewModels.Helpers;
using System.Windows.Input;
using FirstFloor.ModernUI.Presentation;
using System.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using MemberSignInSystem.ModernUI.Content.Dialogs;

namespace MemberSignInSystem.ModernUI.ViewModels.Models
{
    [Serializable]
    public class Family : IComparable<Family>
    {
        private static Random random = new Random();

        public Int32 Id { get; set; }
        public Int32 AccType { get; set; } // 2 - Bond ; 3 - Associate
        public Int32 Flags { get; set; } // 0 - Regular ; 2 - board member ; 4 - inactive ; 8 - referral credits ; 16 - balance due w/ $25 late fee
        public DateTime RenewedDate { get; set; }
        public String FamilyName { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public String PicturePath { get; set; }
        public String Email { get; set; }
        //public List<Individual> Individuals { get; set; }
        public DateTime LoginTime { get; set; }
        public String Time { get { return this.LoginTime.ToShortTimeString(); } }
        public String Date { get { return this.LoginTime.ToShortDateString(); } }

        private String homeNumber = "", cellNumber = "";
        public String HomeNumber
        {
            get
            {
                if (homeNumber != null && homeNumber.Length == 10)
                    return String.Format("({0}) {1}-{2}", homeNumber.Substring(0, 3), homeNumber.Substring(3, 3), homeNumber.Substring(6, 4));
                return homeNumber;
            }
            set
            {
                homeNumber = value.Replace("-", "");
            }
        }
        public String CellNumber
        {
            get
            {
                if (cellNumber != null && cellNumber.Length == 10)
                    return String.Format("({0}) {1}-{2}", cellNumber.Substring(0, 3), cellNumber.Substring(3, 3), cellNumber.Substring(6, 4));
                return cellNumber;
            }
            set
            {
                cellNumber = value.Replace("-", "");
            }
        }

        public Boolean IsHomeNumNotNullOrEmpty { get { return !(homeNumber == null || homeNumber == ""); } }
        public Boolean IsCellNumNotNullOrEmpty { get { return !(cellNumber == null || cellNumber == ""); } }
        public Boolean IsEmailNumNotNullOrEmpty { get { return !(Email == null || Email == ""); } }
        public Boolean IsAddressNumNotNullOrEmpty { get { return !(Address == null || Address == ""); } }

        public String Greeting
        {
            get
            {
                String[] greetings = { "Welcome", "Hello", "Have fun" };
                String[] sponsorMessages = { }; //{ "Stop by Glory Days today for lunch!", "Have you tried Ledo's yet?", "Hungry for some pizza? Why not Vochelli's?" };
                int n = random.Next(greetings.Length + sponsorMessages.Length);

                if (Flags == 0) return (greetings.Concat(sponsorMessages)).ToArray()[n];
                else if (Flags == 2) return "Welcome";
                else if (Flags == 4) return "Account expired";
                else if (Flags == 8) return (greetings.Concat(sponsorMessages)).ToArray()[n];
                else if (Flags == 16) return "Account balance due with 25 dollar late fee.";
                else if (RenewedDate.Year < DateTime.Now.Year) return "Account expired.";
                else return (greetings.Concat(sponsorMessages)).ToArray()[n];;
                /*
                // Membership lasts two years from renewal date.
                TimeSpan timeToExpiration = this.RenewedDate.AddYears(1) - DateTime.Today;

                if (timeToExpiration <= TimeSpan.Zero) // Account expired
                    return "Account expired.";
                else if (timeToExpiration < TimeSpan.FromDays(14)) // Membership will expire within 2 weeks.
                {
                    return "Account expires " + (timeToExpiration.Days == 1 ? "tomorrow." : "in " + (timeToExpiration.Days + " days."));
                }
                else // Account valid.
                {
                    String[] greetings = { "Welcome", "Hello", "Have fun" };
                    String[] sponsorMessages = { }; //{ "Stop by Glory Days today for lunch!", "Have you tried Ledo's yet?", "Hungry for some pizza? Why not Vochelli's?" };
                    int n = random.Next(greetings.Length + sponsorMessages.Length);
                    return (greetings.Concat(sponsorMessages)).ToArray()[n];
                }
                */
            }
        }

        public Brush ColorCode
        {
            get
            {
                if (Flags == 0) return Brushes.Green;
                else if (Flags == 2) return Brushes.Blue;
                else if (Flags == 4) return Brushes.Red;
                else if (Flags == 8) return Brushes.LightGreen;
                else if (Flags == 16) return Brushes.Yellow;
                else if (RenewedDate.Year < DateTime.Now.Year) return Brushes.Red;
                else return Brushes.Green;

                /*
                // Membership lasts two years from renewal date.
                TimeSpan timeToExpiration = this.RenewedDate.AddYears(2) - DateTime.Today;

                if (timeToExpiration <= TimeSpan.Zero) // Account expired
                    return new SolidColorBrush(Colors.Red);
                else if (timeToExpiration < TimeSpan.FromDays(14)) // Membership will expire within 2 weeks.
                // progresses from yellow to red the closer you are to expiration.
                {
                    Color color = Colors.Red;
                    Color backColor = Colors.Yellow;
                    double amount = (14 - timeToExpiration.Days) / 14.0;
                    byte r = (byte)((color.R * amount) + backColor.R * (1 - amount));
                    byte g = (byte)((color.G * amount) + backColor.G * (1 - amount));
                    byte b = (byte)((color.B * amount) + backColor.B * (1 - amount));
                    Color blendedColor = Color.FromRgb(r, g, b);

                    return new SolidColorBrush(blendedColor);
                }
                else // Account valid.
                    return new SolidColorBrush(Colors.Green);
                */
            }
        }


        public Family()
        {
            this.Id = 0;
            this.AccType = 0;
            this.Flags = 0;
            this.RenewedDate = DateTime.MinValue;
            this.FamilyName = "";
            this.Name = "";
            this.Address = "";
            this.HomeNumber = "";
            this.CellNumber = "";
            this.Email = "";
            this.PicturePath = null;
            //this.Individuals = new List<Individual>();
        }
        public Family(Int32 memberID, Int32 type, Int32 flags, DateTime renew, Boolean parent, String famname, String name, String address, String hNum, String cNum, String email)
        {
            this.Id = memberID;
            this.AccType = type;
            this.Flags = flags;
            this.RenewedDate = renew;
            this.FamilyName = famname;
            this.Name = name;
            this.Address = address;
            this.HomeNumber = hNum;
            this.CellNumber = cNum;
            this.Email = email;
            this.PicturePath = FindPicture(this.Id);
            //this.Individuals = GetIndividuals(this.Id);
        }

        public Family(DataRow row)
        {
            this.Id = row["id"] is DBNull ? 0 : Convert.ToInt32(row["id"]);
            this.AccType = row["type"] is DBNull ? 0 : Convert.ToInt32(row["type"]);
            this.Flags = row["flags"] is DBNull ? 0 : Convert.ToInt32(row["flags"]);
            this.RenewedDate = row["renewed"] is DBNull ? DateTime.MinValue : Convert.ToDateTime(row["renewed"]);
            this.FamilyName = row["surname"] is DBNull ? "" : row["surname"] as String;
            this.Name = row["name"] is DBNull ? "" : row["name"] as String;
            this.Address = row["m_addr1"] is DBNull ? "" : row["m_addr1"] as String;
            this.HomeNumber = row["m_phone"] is DBNull ? "" : row["m_phone"] as String;
            this.CellNumber = row["m_mobile"] is DBNull ? "" : row["m_mobile"] as String;
            this.Email = row["m_email"] is DBNull ? "" : row["m_email"] as String;
            this.PicturePath = FindPicture(this.Id);
            //this.Individuals = GetIndividuals(this.Id);
        }

        /*
        private List<Individual> GetIndividuals(String id)
        {
            List<Individual> ret = new List<Individual>();
            DataTable dt = DbHelper.searchByIdForIndividualsWithWildcard(id[id.Length-1] == '.' ? id : id + ".");
            foreach (DataRow row in dt.Rows)
            {
                ret.Add(new Individual(row));
            }
            return ret;
        }
        */

        private String FindPicture(Int32 id)
        {
            String ret = null;
            string[] supportedExts = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            if (Directory.Exists(Application.Current.Resources["RootFolder"] + "\\Member Pictures"))
            {
                foreach (String path in Directory.GetFiles(Application.Current.Resources["RootFolder"] + "\\Member Pictures"))
                {
                    string ext = Path.GetExtension(path);
                    string name = Path.GetFileNameWithoutExtension(path);
                    if (name == Convert.ToString(id) && supportedExts.Contains(ext))
                    {
                        ret = path;
                        break;
                    }
                }
            }
            return ret;
        }

        public static ICommand DisplayInformation
        {
            get
            {
                return new RelayCommand(f =>
                {
                    LoginViewModel viewModel = Application.Current.Resources["LoginViewModel"] as LoginViewModel;
                    viewModel.MemberDisplayVisibilityTimer.Stop();

                    Family fam = f as Family;

                    ModernDialog memberDisplayModernDialog = new ModernDialog()
                    {
                        Title = fam.Id + " " + fam.Name + " " + fam.FamilyName,
                        Content = new MemberDisplayDialog(fam),
                    };
                    memberDisplayModernDialog.Buttons = new List<System.Windows.Controls.Button> { memberDisplayModernDialog.CloseButton };
                    memberDisplayModernDialog.ShowDialog();

                    viewModel.MemberDisplayVisibilityTimer.Start();
                });
            }
        }

        int IComparable<Family>.CompareTo(Family other)
        {
            int retVal = 0;
            // if both this and other have login times, use login time as comparator basis
            if (this.LoginTime != DateTime.MinValue && other.LoginTime != DateTime.MinValue)
            {
                retVal = this.LoginTime.CompareTo(other.LoginTime);
            }
            // if not, or if the two login times are equal, base on family names
            if (retVal == 0)
            {
                retVal = this.FamilyName.CompareTo(other.FamilyName);
            }
            // if the two family names are the same, use the ids
            if (retVal == 0)
            {
                retVal = this.Id.CompareTo(other.Id);
            }
            return retVal;
        }
    }
}
