using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_training.Applications.Adapters;

public interface IRestorer<TDomain, TTarget>
{
    TDomain Restore(TTarget target);
}
