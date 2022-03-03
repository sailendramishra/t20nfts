using System.Collections.Generic;

public class ControllerManager : IManager
{
    private List<IController> controllersList;

    public void Init()
    {
        controllersList = new List<IController>
        {
            new CountrySelectionController(),
            new FormatSelectionController(),
            new PlayerSelectionController(),
            new TossViewController(),
            new StatisticsSelectionController(),
            new GameController(),
            new NotificationController(),
            new GameOverController()
        };

        for (int i = 0; i < controllersList.Count; i++)
        {
            controllersList[i].Init();
        }
    }

    public T GetController<T>() where T : IController
    {
        for (int i = 0; i < controllersList.Count; i++)
        {
            if (controllersList[i] is T controller)
            {
                return controller;
            }
        }

        return default;
    } 
}
