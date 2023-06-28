using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Many_to_Many_and_Inheritance.Classes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Hotel hotel = new Hotel("Canad Inns Destination Centre", "1824 Pembina Hwy, Winnipeg MB");

            Room room1 = new Room("101", 2);
            Room room2 = new Room("102", 4);
            Room room3 = new Room("103", 1);
            VIPRoom room4 = new VIPRoom("104", 3, "Confort", 10);

            Client client1 = new Client("Tom", 1234567890);
            VIPClient client2 = new VIPClient("Jarry", 9876543210, 011);

            Reservation reservation1 = new Reservation(DateTime.Now, 2, client1, room1);
            Reservation reservation2 = new Reservation(DateTime.Now.AddDays(1), 4, client2, room4);

            hotel.Rooms.Add(room1);
            hotel.Rooms.Add(room2);
            hotel.Rooms.Add(room3);
            hotel.Rooms.Add(room4);

            hotel.Clients.Add(client1);
            hotel.Clients.Add(client2);

            room1.Reservations.Add(reservation1);
            room4.Reservations.Add(reservation2);

            Console.WriteLine("Hotel Name: " + hotel.Name);
            Console.WriteLine("Hotel Address: " + hotel.Address);
            Console.WriteLine("Rooms:");
            foreach (Room room in hotel.Rooms)
            {
                Console.WriteLine("Room Number: " + room.Number);
                Console.WriteLine("Capacity: " + room.Capacity);
                Console.WriteLine("Occupied: " + room.Occupied);
                Console.WriteLine("Reservations:");
                foreach (Reservation reservation in room.Reservations)
                {
                    Console.WriteLine("Date: " + reservation.Date);
                    Console.WriteLine("Occupants: " + reservation.Occupants);
                    Console.WriteLine("Is Current: " + reservation.IsCurrent);
                    Console.WriteLine("Client: " + reservation.Client.Name);
                }
                Console.WriteLine();
            }

            Console.WriteLine("Clients:");
            foreach (Client client in hotel.Clients)
            {
                Console.WriteLine("Client Name: " + client.Name);
                Console.WriteLine("Credit Card: " + client.CreditCard);
                Console.WriteLine("Reservations:");
                foreach (Reservation reservation in client.Reservations)
                {
                    Console.WriteLine("Date: " + reservation.Date);
                    Console.WriteLine("Occupants: " + reservation.Occupants);
                    Console.WriteLine("Is Current: " + reservation.IsCurrent);
                    Console.WriteLine("Room Number: " + reservation.Room.Number);
                }
                Console.WriteLine();
            }
        }
    }
}
