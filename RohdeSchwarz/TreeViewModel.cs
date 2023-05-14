using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace RohdeSchwarz
{
    internal class TreeViewModel
    {
        public ObservableCollection<TreeModel> TreeModels { get; set; }

        public TreeViewModel()
        {
            // follow MVVM design pattern
            var tests = DownloadTestData();
            PopulateTreeModels(tests);

        }

        public List<Test> DownloadTestData()
        {
            // generate test data, ideally this is downloading data from DB/API
            var tests = new List<Test>();
            tests.Add(new Test { Major = 1, Minor = 1 });
            tests.Add(new Test { Major = 1, Minor = 2 });
            tests.Add(new Test { Major = 1, Minor = 3 });
            tests.Add(new Test { Major = 1, Minor = 4 });
            tests.Add(new Test { Major = 2, Minor = 1 });
            tests.Add(new Test { Major = 2, Minor = 2 });
            tests.Add(new Test { Major = 2, Minor = 3 });
            return tests;
        }

        public void PopulateTreeModels(List<Test> tests)
        {
            // populate tree model from test data
            TreeModels = new ObservableCollection<TreeModel>();
            var majors = tests.Select(r => r.Major).Distinct().ToList();
            foreach(var major in majors)
            {
                var parent = new TreeModel { Parent = null, Name = $"Test {major}", IsChecked = false };
                var minors = tests.Where(r => r.Major == major).Select(r => r.Minor).ToList();
                foreach(var minor in minors)
                {
                    var child = new TreeModel { Parent = parent, Name = $"Test {major}.{minor}", IsChecked = false };
                    parent.Children.Add(child);
                }
                TreeModels.Add(parent);
            }    
        }
        
    }
}
