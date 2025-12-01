using Raylib_cs;
using static Raylib_cs.Raylib;
namespace Game;
public class Clinic : Workplace
{
    private int _maxPatients;
    private List<Person> _currentPatients;
    private int _healingRate;
    private bool _admitPatientBtn= false;

    public List<Person> CurrentPatients => _currentPatients;
    public int MaxPatients => _maxPatients;

    public Clinic(string name, float xPos, float yPos, int width, int height, Texture2D clinicIcon)
        : base(name, xPos, yPos, width, height, clinicIcon, (int) LandCosts.ClinicWoodCost,(int) LandCosts.ClinicStoneCost)
    {
        _maxPatients = 5;
        _currentPatients = new List<Person>();
        _healingRate = 1;
    }


    public override void RemoveWorker(Person person)
    {
        // Firing a worker
        _currentPeople.Remove(person);
    }
    
    public void Admit(Person person)
    {
        if (_currentPatients.Count < _maxPatients)
        {
            _currentPatients.Add(person);
            person.BecomePatient();
        }
    }
    
    public void Heal()
    {
        if (_currentPeople.Count > 0 && _currentPatients.Count > 0)
        {
            // Logic to apply healing based on _healingRate and workers
            foreach(Person person in _currentPatients)
            {
                person.Heal(_healingRate);
            }
            
            _currentPatients.RemoveAll(p => {
                if (p.CurrentHealth >= p.MaxHealth) {
                    p.RecoverFromIllness();
                    return true;
                }
                return false;
            });
            
        }
    }
    
    public void Release(Person person)
    {
        _currentPatients.Remove(person);
    }

    public void ReleaseAllPeople(){
        base.ReleaseAllPeople();
        _currentPatients.Clear();
    }

    public override void DisplayDetails()
    {
        string clinicStatus = IsOperating ? "Operating" : "Idle";

        Indicator closeIndicator = new Indicator(GetScreenWidth()-45, GetScreenHeight()-290, 45, 45, 1);
        closeIndicator.Draw();
        Util.ScaledDrawTexture(RunTime.CloseIcon, GetScreenWidth()-45, GetScreenHeight()-290, 45);
        Indicator bottomIndicator = new Indicator((GetScreenWidth() / 2) + 200, GetScreenHeight()-250, 800, 250, 3);
        Indicator photoIndicator = new Indicator((GetScreenWidth() / 2) + 200, GetScreenHeight()-44, 262 , 44, 1, 1);
        Indicator indicatorInsideBottom = new Indicator((GetScreenWidth() / 2) + 460, GetScreenHeight()-250, 800 / 3 * 2, 250, 2, 6);
        bottomIndicator.Draw();
        indicatorInsideBottom.Draw();
        photoIndicator.Draw();

        Util.ScaledDrawTexture(Icon, (GetScreenWidth() / 2) + 260, GetScreenHeight()-245, 150);
        // Rectangle woodRect = new Rectangle(ltIndiW /2 + 50, 30, ltIndiH - 40 , 35 );
        Rectangle nameRect = new Rectangle((GetScreenWidth() / 2) + 202, GetScreenHeight()-40, 258 , 40 );
        DrawRectangleRec(nameRect, new Color(255, 204, 106));
        Util.UpdateText(nameRect, Name, (GetScreenWidth() / 2) + 270, GetScreenHeight()-40, 28, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE);

        Util.UpdateText("Type: Building", (GetScreenWidth() / 2) + 480, GetScreenHeight()-200, 28);
        Util.UpdateText($"Status: {clinicStatus}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-200, 28);
        Util.UpdateText($"Health: {CurrentHealth}", (GetScreenWidth() / 2) + 480, GetScreenHeight()-160, 28);
        Util.UpdateText($"Heal Rate: {_healingRate}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-160, 28);
        // Util.UpdateText($"Health: {_currentHealth} ", (GetScreenWidth() / 2) + 480, GetScreenHeight()-160, 28);
        // Util.UpdateText($"Current: Test", (GetScreenWidth() / 2) + 740, GetScreenHeight()-160, 28);
        Util.UpdateText($"Max Patients: {_maxPatients}", (GetScreenWidth() / 2) + 480, GetScreenHeight()-120, 28);
        Util.UpdateText($"Current: {_currentPatients.Count}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-120, 28);

        Util.UpdateText($"Max Workers: {_maxPeople}", (GetScreenWidth() / 2) + 480, GetScreenHeight()-80, 28);
        Util.UpdateText($"Current: {_currentPeople.Count}", (GetScreenWidth() / 2) + 740, GetScreenHeight()-80, 28); 
        // Util.UpdateText("-", (GetScreenWidth() / 2) + 480, GetScreenHeight()-80, 28);
        // Util.UpdateText("-", (GetScreenWidth() / 2) + 740, GetScreenHeight()-80, 28);
        // Util.UpdateText("-", (GetScreenWidth() / 2) + 480, GetScreenHeight()-40, 28);

        Rectangle buttonRect = new Rectangle((GetScreenWidth() / 2) + 460, GetScreenHeight()-45, 265, 45 );
        DrawRectangleRec(buttonRect, Color.Red);
        Util.MakeButton(buttonRect, "Assign",(GetScreenWidth() / 2) + 480, GetScreenHeight()-85, 28, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE, Color.Gold, () => Util.OpenAssignList());
        if(Util.AssignListOpen)
        {
            Util.AssignList(AssignType.WorkPlace, this, "Assign Workers", _currentPeople, _maxPeople, AssignWorker);
        }

        // Util.UpdateText("-", (GetScreenWidth() / 2) + 740, GetScreenHeight()-40, 28); 
        Rectangle admitRect = new Rectangle((GetScreenWidth() / 2) + 725, GetScreenHeight()-45, 235, 45 );
        DrawRectangleRec(admitRect, Color.Blue);
        DrawRectangleLinesEx(admitRect, 2, Color.Black);
        Util.MakeButton(admitRect, "Admit Patient",(GetScreenWidth() / 2) + 740, GetScreenHeight()-40, 28, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE, Color.Gold, () => _admitPatientBtn = true);

        RunTime.detailsShown = true;
        if(GetMousePosition().X > GetScreenWidth()-50 && GetMousePosition().X < GetScreenWidth() &&  GetMousePosition().Y > GetScreenHeight()-290 && GetMousePosition().Y < GetScreenHeight()-240 && IsMouseButtonPressed(MouseButton.Left))
        {
            IsSelected = false;
            RunTime.detailsShown = false;
        }     

        
        if(_admitPatientBtn){
            if(_currentPeople.Count > 0){
                DisplayPatientList();
            }else{
                RunTime.gameScreen.AddMessage("Need Workers", AlertType.WARNING);
                _admitPatientBtn = false;
            }
        }   
    }

    public void DisplayPatientList()
    {

        //ဒီတစ်ခေါက် list of persons က current health က max health ထက်နည်းတဲ့လူတွေကိုပဲပြမယ်။

        int personLimitPerPage = 5;
        int currentPerson = 0;
        int listWidth = 1100;
        int listHeight = 600;
        Rectangle peopleListRect = new Rectangle((GetScreenWidth()  - listWidth ) / 2, (GetScreenHeight() - listHeight) / 2, listWidth, listHeight);
        DrawRectangleRec(peopleListRect,  new Color(200, 200, 200, 225));

        Util.UpdateText(peopleListRect, "\nAdmit Patients", (int) peopleListRect.X, (int) peopleListRect.Y + 20, 30, (int) TextAlign.TEXT_ALIGN_MIDDLE, (int) TextAlign.TEXT_ALIGN_TOP);

        Rectangle closeBtnRect = new Rectangle(peopleListRect.X + peopleListRect.Width - 45, peopleListRect.Y, 45, 45);
        DrawRectangleRec(closeBtnRect, new Color(255, 100, 100, 200));
        Util.ScaledDrawTexture(RunTime.CloseIcon, closeBtnRect.X, closeBtnRect.Y, 45);

        if(GetMousePosition().X > closeBtnRect.X && GetMousePosition().X < closeBtnRect.X + closeBtnRect.Width &&  GetMousePosition().Y > closeBtnRect.Y && GetMousePosition().Y < closeBtnRect.Y + closeBtnRect.Height && IsMouseButtonPressed(MouseButton.Left))
        {
            _resourcePersons.Clear();
            _admitPatientBtn = false;
        }

        Rectangle personOne = new Rectangle(peopleListRect.X + 50, peopleListRect.Y + 80, peopleListRect.Width - 90, 70);
        DrawRectangleRec(personOne, new Color(217, 217, 219, 255));

        Rectangle personTwo = new Rectangle(peopleListRect.X + 50, peopleListRect.Y + 170, peopleListRect.Width - 90, 70);
        DrawRectangleRec(personTwo, new Color(217, 217, 219, 255));

        Rectangle personThree = new Rectangle(peopleListRect.X + 50, peopleListRect.Y + 260, peopleListRect.Width - 90, 70);
        DrawRectangleRec(personThree, new Color(217, 217, 219, 255));

        Rectangle personFour = new Rectangle(peopleListRect.X + 50, peopleListRect.Y + 350, peopleListRect.Width - 90, 70);
        DrawRectangleRec(personFour, new Color(217, 217, 219, 255));

        Rectangle personFive = new Rectangle(peopleListRect.X + 50, peopleListRect.Y + 440, peopleListRect.Width - 90, 70);
        DrawRectangleRec(personFive, new Color(217, 217, 219, 255));

        Rectangle[] personRectangles = new Rectangle[] { personOne, personTwo, personThree, personFour, personFive };

        // List<ResourcePerson> resourcePersons = new List<ResourcePerson>();
        List<Person> persons = RunTime.gameScreen.GetPersonLists().Where(p => p.CurrentHealth < p.MaxHealth && p.IsFainted == false).ToList();
        bool personExists = false;
        foreach (Person person in persons)
        {
            ResourcePerson resourcePerson = new ResourcePerson(person, person.IsWorking);

            foreach (ResourcePerson rp in _resourcePersons)
            {
                if (rp.Person == person)
                {
                    personExists = true;
                    break;
                }
            }

            if(personExists == false)
            {
                _resourcePersons.Add(resourcePerson);
            }

        }

        int personCountTotal = _resourcePersons.Count;
        int pageIndex = Math.Max(0, RunTime.gameScreen.CurrentPersonDisplayPage);
        int startIndex = pageIndex * personLimitPerPage;

        for (int slot = 0; slot < personLimitPerPage; slot++)
        {
            int personIndex = startIndex + slot;
            if (personIndex >= personCountTotal) break;
            // Person person = persons[personIndex];
            ResourcePerson resourcePerson = _resourcePersons[personIndex];
            Person person = resourcePerson.Person;
            currentPerson++;
            string idleStatus = person.IsWorking ? "Working" : "Idle";

            Util.ScaledDrawTexture(RunTime.PersonDown, personRectangles[slot].X + 10, personRectangles[slot].Y + 10, 50);

            Rectangle personNameRect = new Rectangle((int) personRectangles[slot].X + 70, (int) personRectangles[slot].Y + 10, 200, 50);
            DrawRectangleRec(personNameRect, new Color(255, 204, 106, 255));
            Util.UpdateText(personNameRect, person.Name, (int) personRectangles[slot].X + 80, (int) personRectangles[slot].Y + 20, 30, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE);

            Rectangle statusRect = new Rectangle((int) personRectangles[slot].X + 300, (int) personRectangles[slot].Y + 10, 200, 50);
            DrawRectangleRec(statusRect, new Color(255, 204, 106, 255));
            Util.UpdateText(statusRect, idleStatus, (int) personRectangles[slot].X + 310, (int) personRectangles[slot].Y + 20, 30, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE);

            Rectangle energyRect = new Rectangle((int) personRectangles[slot].X + 520, (int) personRectangles[slot].Y + 10, 250, 50);
            DrawRectangleRec(energyRect, new Color(255, 204, 106, 255));
            Util.UpdateText(energyRect, $"E: {person.CurrentEnergy}%, H: {person.CurrentHealth}", (int) personRectangles[slot].X + 560, (int) personRectangles[slot].Y + 20, 30, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE);

            Rectangle eachPersonTickRect = new Rectangle((int) personRectangles[slot].X + 900, (int) personRectangles[slot].Y + 15, 40, 40);
            DrawRectangleRec(eachPersonTickRect, new Color(215, 217, 219, 255));
            DrawRectangleLinesEx(eachPersonTickRect, 2, Color.Black);


            if(person.IsHealing)
            {
                Util.ScaledDrawTexture(RunTime.greenTickIcon, eachPersonTickRect.X, eachPersonTickRect.Y, 40);
            }
            else
            {
                if (resourcePerson.IsSelected)
                {
                    Util.ScaledDrawTexture(RunTime.tickIcon, eachPersonTickRect.X, eachPersonTickRect.Y, 40);
                }
                if(GetMousePosition().X > eachPersonTickRect.X && GetMousePosition().X < eachPersonTickRect.X + eachPersonTickRect.Width &&  GetMousePosition().Y > eachPersonTickRect.Y && GetMousePosition().Y < eachPersonTickRect.Y + eachPersonTickRect.Height && IsMouseButtonPressed(MouseButton.Left))
                {
                    _randomAssign = false;
                    resourcePerson.IsSelected = !resourcePerson.IsSelected;
                    // Util.ScaledDrawTexture(RunTime.tickIcon, eachPersonTickRect.X, eachPersonTickRect.Y, 40);
                }
            }

        }

        Rectangle tickRect = new Rectangle((listWidth / 2) + 200, peopleListRect.Y + peopleListRect.Height - 65, 45, 45);
        DrawRectangleRec(tickRect, new Color(215, 217, 219, 255));
        DrawRectangleLinesEx(tickRect, 2, Color.Black);
        if(GetMousePosition().X > tickRect.X && GetMousePosition().X < tickRect.X + tickRect.Width &&  GetMousePosition().Y > tickRect.Y && GetMousePosition().Y < tickRect.Y + tickRect.Height && IsMouseButtonPressed(MouseButton.Left))
        {
            _randomAssign = !_randomAssign;
            foreach (ResourcePerson rp in _resourcePersons)
            {
                rp.IsSelected = false;
            }
        }

        if(_randomAssign)
        {
            Util.ScaledDrawTexture(RunTime.tickIcon, tickRect.X, tickRect.Y, 45);
        }
        // Util.ScaledDrawTexture(RunTime.tickIcon, tickRect.X, tickRect.Y, 45);
        Util.UpdateText("Random", (int)(tickRect.X + 60), (int)(tickRect.Y + 10), 28);


        Rectangle confirmRect = new Rectangle(tickRect.X + 200, peopleListRect.Y + peopleListRect.Height - 65, 200, 45);
        DrawRectangleRec(confirmRect, Color.Red );
        DrawRectangleLinesEx(confirmRect, 2, Color.Black);
        Util.UpdateText(confirmRect, "Confirm", (int)confirmRect.X + 100, (int)confirmRect.Y + 10, 28, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE, Color.White);

        if(GetMousePosition().X > confirmRect.X && GetMousePosition().X < confirmRect.X + confirmRect.Width &&  GetMousePosition().Y > confirmRect.Y && GetMousePosition().Y < confirmRect.Y + confirmRect.Height && IsMouseButtonPressed(MouseButton.Left))
        {
            if(_currentPatients.Count == _maxPatients)
            {
                _admitPatientBtn = false;
                RunTime.gameScreen.AddMessage("Patients are already full.", AlertType.ERROR);
            }else if(_currentPatients.Count < _maxPatients)
            {
                int patientsAdmittable = _maxPatients - _currentPatients.Count;
                if (!_randomAssign)
                {
                    int patientsSelected = _resourcePersons.Where(rp => rp.IsSelected).Count();
                    // Console.WriteLine($"Workers Selected: {workersSelected}, Workers Needed: {workersNeeded}");
                    if(patientsSelected > patientsAdmittable)
                    {
                        RunTime.gameScreen.AddMessage("Too many patients", AlertType.ERROR);
                    }
                    else if(patientsSelected == 0)
                    {
                        RunTime.gameScreen.AddMessage("No patients selected", AlertType.WARNING);
                    }
                    else
                    {
                       foreach (ResourcePerson rp in _resourcePersons)
                       {
                           if(rp.IsSelected)
                           {
                               Admit(rp.Person);
                           }
                       }
                         _resourcePersons.Clear();
                          _admitPatientBtn = false;
                    }
                }
                else
                {
                    List<Person> randomPatients = _resourcePersons.Select(rp => rp.Person).ToList();
                    Random rand = new Random();
                    randomPatients = randomPatients.OrderBy(x => rand.Next()).ToList(); 

                    for (int i = 0; i < Math.Min(patientsAdmittable, randomPatients.Count); i++)
                    {
                        Admit(randomPatients[i]);
                    }
                    _resourcePersons.Clear();
                    _admitPatientBtn = false;
                }

            }
        }

        if ((pageIndex + 1) * personLimitPerPage < personCountTotal)
        {
            Rectangle nextBtn = new Rectangle(peopleListRect.X + peopleListRect.Width - 150, peopleListRect.Y + peopleListRect.Height - 70, 140, 50);
            DrawRectangleRec(nextBtn, new Color(217, 217, 219, 255));
            DrawRectangleLinesEx(nextBtn, 2, Color.Black);

            Util.MakeButton(nextBtn, "Next", (int)(peopleListRect.X + peopleListRect.Width - 160), (int)(peopleListRect.Y + peopleListRect.Height - 60), 28, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE, Color.Black, () => {
                RunTime.gameScreen.CurrentPersonDisplayPage += 1;
            });
        }

        if (pageIndex > 0)
        {
            Rectangle prevBtn = new Rectangle(peopleListRect.X + 10, peopleListRect.Y + peopleListRect.Height - 70, 140, 50);
            DrawRectangleRec(prevBtn, new Color(217, 217, 219, 255));
            DrawRectangleLinesEx(prevBtn, 2, Color.Black);
            Util.MakeButton(prevBtn, "Prev", (int)prevBtn.X, (int)prevBtn.Y, 28, (int)TextAlign.TEXT_ALIGN_CENTRE, (int)TextAlign.TEXT_ALIGN_MIDDLE, Color.Black, () => {
                if (RunTime.gameScreen.CurrentPersonDisplayPage > 0) RunTime.gameScreen.CurrentPersonDisplayPage--;
            });
        }
    }

    // public void DisplayPeopleList(){
    //     Console.WriteLine("Displaying people list for assignment.");
    // }
}