using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace war
{
    class Battle
    {
        int _raund = 0;

        private Squad _squadLeft;
        private Squad _squadRight;

        public Battle()
        {
            _raund = 1;
            _squadLeft = new Squad();
            _squadRight = new Squad();
        }

        public void RunMenu()
        {
            bool isRun = true;
            while (isRun)
            {
                Console.WriteLine("[1] - Создать отряд\n[2] - Запустить бой\n[3] - Выход");

                switch (Console.ReadLine())
                {
                    case "1":
                        _squadLeft.CreateSquad();
                        _squadRight.CreateSquad();
                        break;
                    case "2":
                        StartGame();
                        break;
                    case "3":
                        isRun = false;
                        break;
                }
            }
        }

        private void StartGame()
        {
            if (IsGameReady() == false)
            {
                return;
            }

            while (IsGameOver() == false)
            {
                MakeRound();

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void MakeRound()
        {
            InitStage();
            FightStage();
            EndStage();
        }

        public void InitStage()
        {
            _squadLeft.ChooseRandomFrontmen();
            _squadRight.ChooseRandomFrontmen();
        }

        private void FightStage()
        {
            Console.WriteLine("Западный отряд атакует!");
            _squadRight.TakeAction(_squadLeft);

            Console.WriteLine("Восточный отряд атакует!");
            _squadLeft.TakeAction(_squadRight);
        }

        private void EndStage()
        {
            _squadLeft.TryDeleteDeadSoldier();
            _squadRight.TryDeleteDeadSoldier();

            if(IsSquadsReady())
            {
                Console.WriteLine("Новый раунд!");
                _raund++;
            }
        }

        private bool IsSquadsReady()
        {
            if (_squadLeft.IsSoldiersUnready() && _squadRight.IsSoldiersUnready())
            {
                _squadLeft.Refresh();
                _squadRight.Refresh();
                return true;
            }
            return false;
        }

        private bool IsGameOver()
        {
            if (_squadLeft.GetHealthSquad() <= 0)
            {
                Console.WriteLine("Западный отряд умер!");
                return true;
            }
            else if (_squadRight.GetHealthSquad() <= 0)
            {
                Console.WriteLine("Восточный отряд умер!");
                return true;
            }

            Console.WriteLine("Raund: " + _raund);
            Console.WriteLine("\nСилы западного отряда: " + _squadLeft.GetHealthSquad());
            Console.WriteLine("Силы восточного отряда: " + _squadRight.GetHealthSquad() + "\n");
            return false;
        }

        private bool IsGameReady()
        {
            if (_squadLeft.GetSquadSize() < 0)
            {
                Console.WriteLine("Левый отряд не создан!");
                return false;
            }
            else if (_squadRight.GetSquadSize() < 0)
            {
                Console.WriteLine("Правый отряд не создан!");
                return false;
            }
            return true;
        }
    }
}
