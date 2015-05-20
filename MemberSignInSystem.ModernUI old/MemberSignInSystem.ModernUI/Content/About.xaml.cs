using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MemberSignInSystem.ModernUI.Content
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : UserControl
    {
        public About()
        {
            InitializeComponent();

            String applicationInfo =
                "My name is Nathan Williams. In my senior year at " +
                "Thomas Jefferson High School for Science and Technology (2013-14), " +
                "I developed this application with the guidance of Paul Maingault and other board members " +
                "of the Rolling Valley Swim and Tennis Club (RVSTC). " +
                "As somebody who has devoted an extensive amount of time into numerous computer science (CS) projects, " +
                "I would encourage anyone still studying to at least seek exposure to the field of CS. " +
                "Within the world of CS and application development is a rich abundance of opportunity " +
                "interesting topics just waiting to stimulate the minds of any generation. \n\n" +

                "My motivation for approaching this project went beyond my attachments to CS, though. " +
                "Rolling Valley has been the home of my childhood, especially after I joined the Swim Team " +
                "in the summer of 2007. I love this pool, and I just hope that this application can help make " +
                "the pool experience just the tiniest bit more enjoyable for everyone who notices it, both today " +
                "and as the application evolves in the future. \n\n" ;
            String setupHelp =
                "If this is your first time using this Application and you want some help setting up, here are some " +
                "instructions: \n" +
                "TODO" +
                "" ;
            InfoText.Text = applicationInfo;
        }
    }
}
