using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RohdeSchwarz
{
    public class TreeModel: INotifyPropertyChanged
    {
        private bool? is_checked;

        public TreeModel() 
        {
            this.Children = new ObservableCollection<TreeModel> ();
        }

        public TreeModel? Parent { get; set; }
        public string? Name { get; set; }   
        public bool? IsChecked 
        {
            get
            {
                // get is checked prop
                if (Parent != null)
                {
                    if (Parent.Children.Count > 0)
                    {
                        Parent.is_checked = null; // undetermined state by default
                        // get all parents children checked/unchecked
                        var all_checked = Parent.Children.All(c => c.is_checked == true);
                        var all_unchecked = Parent.Children.All(c => c.is_checked == false);
                        // checked parent if all children checked
                        if (all_checked)
                            Parent.is_checked = true;       
                        // uncheck parent if all children unchecked
                        if (all_unchecked)
                            Parent.is_checked = false;
                    }
                    Parent.OnPropertyChanged(nameof(Parent.IsChecked));     
                }
                return is_checked;
            } 
            set
            {
                // set is checked property
                if (is_checked != value)
                {
                    is_checked = value;
                    // raise prop change event to update ui
                    OnPropertyChanged(nameof(IsChecked));
                    if (Children.Count > 0)
                    {
                        // set all children prop to parent is checked
                        foreach(var child in Children)
                        {
                            child.IsChecked = value;
                        }
                    }
                }
            }
        }
        public ObservableCollection<TreeModel> Children { get; set; }

        // define prop change event handler and args
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName=null) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
