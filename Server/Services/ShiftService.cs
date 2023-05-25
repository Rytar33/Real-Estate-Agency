using Server.Context;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    public class ShiftService
    {
        public int IDWorker { get; set; }
        public ShiftService(int idWorker) => IDWorker = idWorker;
        public void StartShift()
        {
            using var db = new DataBaseContext();
            var lastShift = db.Shift.Where(s => s.Worker.IDWorker == IDWorker).ToList();

            if (lastShift != null && lastShift.Count > 0)
            {
                var shift = lastShift.TakeLast(1).FirstOrDefault();
                if(shift.EndShift == null)
                {
                    Console.WriteLine("Прошлая смена не была окончена! Окончите её.");
                    return;
                }
            }
            var worker = db.Worker.FirstOrDefault(w => w.IDWorker == IDWorker);
            db.Shift.Add(new Shift() { Worker = worker, StartShift = DateTime.Now });
            db.SaveChanges();
        }
        public void StopShift() 
        {
            using var db = new DataBaseContext();
            var lastShift = db.Shift.Where(s => s.Worker.IDWorker == IDWorker).ToList();
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