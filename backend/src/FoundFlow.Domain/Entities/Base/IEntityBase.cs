using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoundFlow.Domain.Entities.Base;
public interface IEntityBase<out TId>
{
    TId Id { get; }
}
