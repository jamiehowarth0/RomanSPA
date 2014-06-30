using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RomanSPA.Core.Models {
    public interface IModelFactory {
        object Execute();
    }
}
