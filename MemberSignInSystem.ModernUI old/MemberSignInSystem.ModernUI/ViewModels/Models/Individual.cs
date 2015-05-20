/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Data;
using System.IO;
using System.Windows.Media.Imaging;

using FirstFloor.ModernUI.Presentation;
using MemberSignInSystem.ModernUI.ViewModels.Models;
using System.Windows.Input;
using FirstFloor.ModernUI.Windows.Controls;
using MemberSignInSystem.ModernUI.Content.Dialogs;
using System.Windows;

namespace MemberSignInSystem.ModernUI.ViewModels.Models
{
    [Serializable]
    public class Individual
    {
        public String Id { get; set; }
        public Boolean IsParent { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public DateTime Birthdate { get; set; }
        
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
                homeNumber = value.Replace("-","");
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
        
        public Boolean PassedSwimTest { get; set; }
        public String PicturePath { get; set; }

        public Individual()
        {
            this.Id = "";
            this.IsParent = false;
            this.FirstName = "";
            this.LastName = "";
            this.Birthdate = DateTime.MinValue;
            this.HomeNumber = "";
            this.CellNumber = "";
            this.PassedSwimTest = false;
            this.PicturePath = null;
        }
        public Individual(String memberID, Boolean parent, String fname, String lname, DateTime bdate, String home, String cell, Boolean swimtest)
        {
            this.Id = memberID;
            this.IsParent = parent;
            this.FirstName = fname;
            this.LastName = lname;
            this.Birthdate = bdate;
            this.HomeNumber = home;
            this.CellNumber = cell;
            this.PassedSwimTest = swimtest;
            this.PicturePath = FindPicture(this.Id);
        }

        public Individual(DataRow row)
        {
            this.Id = row["id"] as String;
            this.IsParent = Convert.ToBoolean(row["parent"]);
            this.FirstName = row["firstname"] as String;
            this.LastName = row["lastname"] as String;
            this.Birthdate = Convert.ToDateTime(row["birthdate"]);
            this.HomeNumber = row["home"] as String;
            this.CellNumber = row["cell"] as String;
            this.PassedSwimTest = Convert.ToBoolean(row["swimtest"]);
            this.PicturePath = FindPicture(this.Id);
        }

        private String FindPicture(string id)
        {
            String ret = null;
            string directoryUri = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "Membership Records\\");
            foreach (String path in Directory.GetFiles(directoryUri + "Member Pictures\\"))
            {
                string ext = Path.GetExtension(path);
                string name = Path.GetFileNameWithoutExtension(path);
                string[] supportedExts = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
                if (name == id && supportedExts.Contains(ext))
                {
                    ret = path;
                }
            }
            return ret;
        }

        public Boolean YoungerThan12
        {
            get
            {
                return ((DateTime.Now - this.Birthdate).Days / 365.25) < 12;
            }
        }
        public Boolean IsHomeNumNotNullOrEmpty { get { return !(homeNumber == null || homeNumber == ""); } }
        public Boolean IsCellNumNotNullOrEmpty { get { return !(cellNumber == null || cellNumber == ""); } }

        public String Age
        {
            get
            {
                return ((int)((DateTime.Now - this.Birthdate).Days / 365.25)).ToString();
            }
        }

        public static ICommand DisplayInformation
        {
            get
            {
                return new RelayCommand(i =>
                {
                    LoginViewModel viewModel = Application.Current.Resources["LoginViewModel"] as LoginViewModel;
                    viewModel.MemberDisplayVisibilityTimer.Stop();

                    Individual ind = i as Individual;
                    Console.Write(ind.Id + " clicked.");

                    ModernDialog memberDisplayModernDialog = new ModernDialog()
                    {
                        Title = ind.Id + " " + ind.FirstName + " " + ind.LastName,
                        Content = new MemberDisplayDialog(ind),
                    };
                    memberDisplayModernDialog.Buttons = new List<System.Windows.Controls.Button> { memberDisplayModernDialog.CloseButton };
                    memberDisplayModernDialog.ShowDialog();

                    viewModel.MemberDisplayVisibilityTimer.Start();
                });
            }
        }
    }
}
*/