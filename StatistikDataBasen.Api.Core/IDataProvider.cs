using System.Collections.Generic;

namespace StatistikDataBasen.Api.Core
{
    public interface IDataProvider<T>
    {
        IEnumerable<T> GetDataPoints();
    }
}
