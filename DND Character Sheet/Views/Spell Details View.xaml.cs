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
using System.Windows.Shapes;
using DND_Character_Sheet.Models.Serialize_Types;

namespace DND_Character_Sheet.Views
{
    /// <summary>
    /// Interaction logic for Spell_Details_View.xaml
    /// </summary>
    public partial class SpellDetailsView : Window
    {
        public SpellDetailsView(Spell spell)
        {
            this.DataContext = spell;
            InitializeComponent();
        }
    }
}
