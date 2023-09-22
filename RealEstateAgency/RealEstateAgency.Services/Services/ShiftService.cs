using RealEstateAgency.DBMigrations;
using RealEstateAgency.Models;

namespace RealEstateAgency.Services.Services
{
    public class ShiftService
    {
        public int IDWorker { get; set; }
        public ShiftService(int idWorker) => IDWorker = idWorker;
        public void StartShift()
        {
            using var db = new DataBaseContext();
            var lastShift = db.Shift.Where(s => s.WorkerIDWorker == IDWorker).ToList();

            if (lastShift != null && lastShift.Count > 0)
            {
                var shift = lastShift.TakeLast(1).FirstOrDefault();
                if (shift!.EndShift == null)
                {
                    Console.WriteLine("Прошлая смена не была окончена! Окончите её.");
                    return;
                }
            }
            var worker = db.Worker.FirstOrDefault(w => w.IDWorker == IDWorker);
            db.Shift.Add(new Shift() { WorkerIDWorker = worker!.IDWorker, StartShift = DateTime.Now });
            db.SaveChanges();
        }
        public void StopShift()
        {
            using var db = new DataBaseContext();
            var lastShift = db.Shift.Where(s => s.WorkerIDWorker == IDWorker).ToList();
            Shift shift = new Shift();

            if (lastShift != null && lastShift.Count > 0) shift = lastShift.TakeLast(1).FirstOrDefault()!;
            if (lastShift == null || lastShift.Count == 0 || shift.EndShift != null)
            {
                Console.WriteLine("Вы не начали смену.");
                return;
            }
            shift.EndShift = DateTime.Now;
            db.SaveChanges();
        }
    }
}