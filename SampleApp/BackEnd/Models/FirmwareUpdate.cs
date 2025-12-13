namespace InterviewProject.Models;

public class FirmwareUpdate
{
    public Guid DeviceId;
    public int Progress;
    public string State; // "Active", "Completed", "Failed" ...
    public DateTime LastChanged;
    public string Notes;
}
