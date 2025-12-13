using InterviewProject.Models;


namespace InterviewProject;

public class FirmwareUpdateService
{
    private readonly IServiceScopeFactory scopeFactory;

    public FirmwareUpdateService(IServiceScopeFactory scopeFactory)
    {
        this.scopeFactory = scopeFactory;
    }
    public async Task<string> UpdateProgress(Guid deviceId, int progress, string notes)
    {
        IServiceScope scope = this.scopeFactory.CreateScope();
        IFirmwareUpdateRepository repo = scope.ServiceProvider.GetService<IFirmwareUpdateRepository>();
        ILogger logger = scope.ServiceProvider.GetService<ILogger>();

        FirmwareUpdate update = repo.Get(deviceId);

        if (update == null)
        {
            update = new FirmwareUpdate();
            update.DeviceId = deviceId;
            update.Progress = 0;
            update.State = "Active";
        }

        update.Progress = progress;
        update.Notes = notes;
        update.LastChanged = DateTime.Now;

        repo.CreateAndSave(update);

        FirmwareUpdate active = repo.GetAll()
            .Where(x => x.DeviceId == deviceId)
            .OrderByDescending(x => x.LastChanged)
            .FirstOrDefault();

        int total = 100;

        if (progress == total)
        {
            active.State = "Completed";
            repo.CreateAndSave(active); // again duplicates instead of update
            return "DONE"; // In general its weird to return a string for a result
        }

        return active.State;
    }
}
