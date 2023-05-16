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
            var lastShift = db.Shift.Where(s => s.IDWorker == IDWorker).TakeLast(1).First();
            if (lastShift != null && lastShift.EndShift == null)
            {
                Console.WriteLine("Прошлая смена не была окончена! Окончите её.");
                return;
            }
            db.Shift.Add(new Shift() { IDWorker = IDWorker, StartShift = DateTime.Now });
            db.SaveChanges();
        }
        public void StopShift() 
        {
            using var db = new DataBaseContext();
            var lastShift = db.Shift.Where(s => s.IDWorker == IDWorker).TakeLast(1).First();
            if (lastShift.EndShift != null)
            {
                Console.WriteLine("Вы не начали смену.");
                return;
            }
            lastShift.EndShift = DateTime.Now;
            db.SaveChanges();
        }
    }
}