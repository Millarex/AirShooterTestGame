using UnityEngine;
using UnityEngine.Events; // 1

public class GameEventListener : MonoBehaviour
{
    [SerializeField]
    private GameEvent gameEvent; // 2
    [SerializeField]
    private UnityEvent response; // 3

    private void OnEnable() // 4
    {
        gameEvent.RegisterListener(this);
    }

    private void OnDisable() // 5
    {
        gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised() // 6
    {
        response.Invoke();
    }
}
/*
В показанном выше коде происходит дальнейшее развитие проекта:

1 - Требование использования класса UnityEvent.
2 - GameEvent, на который будет подписан этот GameEventListener.
3 - Отклик UnityEvent, который будет вызываться, при генерации событием GameEvent этого GameEventListener.
4 - Привязка GameEvent к GameEventListener, когда этот GameObject включен.
5 - Отвязка GameEvent от GameEventListener, когда этот GameObject отключен.
6 - Вызывается при генерации GameEvent, приводящей к вызову слушателем GameEventListener события UnityEvent.
*/