using InterviewProject.Models;


namespace InterviewProject;

public interface IFirmwareUpdateRepository
{
    FirmwareUpdate Get(Guid deviceId);
    void CreateAndSave(FirmwareUpdate update);
    List<FirmwareUpdate> GetAll();
}

public class FirmwareUpdateRepository : IFirmwareUpdateRepository
{
    private static readonly List<FirmwareUpdate> Db = new List<FirmwareUpdate>();

    public FirmwareUpdate Get(Guid deviceId)
    {
        return Db.FirstOrDefault(x => x.DeviceId == deviceId);
    }

    public void CreateAndSave(FirmwareUpdate update)
    {
        Db.Add(update);
    }

    public List<FirmwareUpdate> GetAll()
    {
        return Db;
    }
}
