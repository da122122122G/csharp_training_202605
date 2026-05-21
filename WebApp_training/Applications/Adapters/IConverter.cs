using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_training.Applications.Adapters;

public interface IConverter<TDomain, TTarget>
{
    TTarget Convert(TDomain domain);
}