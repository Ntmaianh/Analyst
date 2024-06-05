namespace FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly
{
    public interface ICurrentUser
    {
        Guid? Id { get; }
        string UserName { get; }
        string Name { get; }
        string Email { get; }
        string Picture { get; }

        List<string> RoleCodes { get; }
        List<string> RoleNames { get; }
        Guid? IdTrainingFacility { get; }
    }
}
