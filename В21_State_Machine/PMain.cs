using System;
using System.Collections.Generic;
using static System.Console;

namespace В21_HandmadeStateMachine
{
    public enum State 
    {
        OffHook,    //Снята трубка
        Connecting, //Подключение
        Connected,  //Соединено
        OnHold      //На удерживании
    }

    public enum Trigger 
    {
        CallDialed,   //Звонок набран
        HungUp,       //Повесить трубку
        CallConected, //Вызов подключен
        PlacedOnHold, //Помещено на удержание
        TakenOffHold, //Снято с удержания
        LeftMessage   //Оставленное сообщение
    }

    class PMain
    {
        private static Dictionary<State, List<(Trigger, State)>> rules
            = new Dictionary<State, List<(Trigger, State)>>
            {
                [State.OffHook] = new List<(Trigger, State)>
                {
                    (Trigger.CallDialed,State.Connecting) //Номер набран-подключение
                },

                [State.Connecting] = new List<(Trigger, State)>
                {
                    (Trigger.HungUp,State.OffHook),          // Сброс - снята трубка
                    (Trigger.CallConected,State.Connected)   // Сопряжено - подключен
                },

                [State.Connected] = new List<(Trigger, State)>
                {
                    (Trigger.LeftMessage,State.OffHook), // Сообщение оставлено - снята трубка
                    (Trigger.HungUp,State.OffHook),      // Сброс - снята трубка
                    (Trigger.PlacedOnHold,State.OnHold)  // Помещенно на удержание - на удержании 
                },

                [State.OnHold] = new List<(Trigger, State)>
                {
                    (Trigger.TakenOffHold,State.Connected), //Снять с удержания - Соединено
                    (Trigger.HungUp,State.OffHook)          //Сброс - снята трубка
                }
            };

        static void Main(string[] args)
        {
            var state = State.OffHook;
            while (true)
            {
                WriteLine($"The phone is currectly {state}");
                WriteLine("Select a trigger:");

                //foreach to for
                for (var i = 0; i < rules[state].Count; i++)
                {
                    var (t, _) = rules[state][i];
                    WriteLine($"{i}. {t}");
                }

                int input = int.Parse(ReadLine());

                var (_, s) = rules[state][input];
                state = s;
            }
        }
    }
}
