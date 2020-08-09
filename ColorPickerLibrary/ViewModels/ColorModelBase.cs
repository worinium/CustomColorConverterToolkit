using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPickerLibrary.ViewModels
{
    public abstract class ColorModelBase: BindableBase
    {
        protected readonly ColorViewerModalViewModel mainVm;

        public abstract void ReadOurColorFromRGB();
        public abstract void UpdateColorFromRGB();

        public ColorModelBase(ColorViewerModalViewModel mainVm)
        {
            this.mainVm = mainVm;
            ReadOurColorFromRGB();
        }
    }
}
