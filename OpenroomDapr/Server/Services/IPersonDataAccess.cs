using OpenroomDapr.Shared.Model;

namespace OpenroomDapr.Server.Services;

public interface IPersonDataAccess
{
    Task<IEnumerable<MyPerson>> GetPersons(CancellationToken cancellationToken);
    Task<MyInteger> UpdateMyPerson(MyPerson person, CancellationToken cancellationToken);
}
