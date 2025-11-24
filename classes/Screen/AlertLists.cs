namespace Game;
public class AlertList
{
    private List<Alert> _alerts;

    public AlertList()
    {
        _alerts = new List<Alert>();
    }

    public void AddAlert(Alert alert)
    {
        if(_alerts.Count <= 3)
        {
            _alerts.Add(alert);
            alert.SetNewAlertId(_alerts.Count);
        }
        else
        {
            _alerts.RemoveAt(0);
            for(int i = 0; i < _alerts.Count; i++)
            {
                _alerts[i].SetNewAlertId(i + 1);
            }
            _alerts.Add(alert);
            alert.SetNewAlertId(_alerts.Count);
        }

    }

    public void RemoveAlert(Alert alert)
    {
        _alerts.Remove(alert);
    }

    public List<Alert> GetAllAlerts()
    {
        return _alerts;
    }   

    public void DisplayAllAlerts()
    {
        if(_alerts.Count != 0)
        {
            foreach (Alert alert in _alerts)
            {
                alert.Draw();
            }
            _alerts.RemoveAll(a => a.IsExpired());
        }

    }
}