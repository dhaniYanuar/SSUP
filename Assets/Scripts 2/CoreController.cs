using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class CoreController : MonoBehaviour {
    public enum ScheduleState { Closed, Talks, Works, Match }
    #region Variable
    private const int ENUM_STATUS_NOT_LOGGED_IN = 0;
    private const int ENUM_STATUS_LOGGED_IN = 1;

    [SerializeField] ScheduleState scheduleState = ScheduleState.Closed;

    [SerializeField] InputField inputLoginName, inputLoginPassword;
    [SerializeField] InputField inputSignUpName, inputSignUpBirthday, inputSignUpEmail, inputSignUpPhoneNumber, inputSignUpOcupation, inputSignUpPassword, inputSignUpConfirmPassword;
    [SerializeField] List<Text> listOfStarText;
    [SerializeField] Text txtSignUpFeedback;

    [SerializeField] GameObject canvasLogIn, canvasSplashScreen, canvasSignUpOption, canvasSignUp;

    [SerializeField] GameObject canvasHome, canvasInformation, canvasEvents, canvasSchedule, canvasArMenu;

    [SerializeField] Image imgScheduleContent;
    [SerializeField] Button btnStartTalk, btnStartWorks, btnStartMatch;

    [SerializeField] TouchDetector touchDetector;
    [SerializeField] List<Image> listOfImgSlider;
    [SerializeField] Image imgSliderCircle;
    [SerializeField] List<Sprite> listOfSliderCircleSprite;
    [SerializeField] List<Sprite> listOfScheduleContentSprite;

    [SerializeField] string arSceneName, treasureHuntSceneName;

    private bool isGuest, isLoggedIn;

    private bool isSwiping, isSliding = false;
    private int sliderImageId = 0;
    public static int IdUI_AR;
    public GameObject UIScore;
    #endregion

    #region variable_database


    private string url_register = "http://testlaravel7.dev.ent.pens.ac.id/public/";
    private string url_login = "http://testlaravel7.dev.ent.pens.ac.id/public/login";
    private string url_update = "http://testlaravel7.dev.ent.pens.ac.id/public/update";
    public string status;
    protected static User myUser;
    WWW www;
    WWWForm form;
    #endregion 

    // Use this for initialization
    void Start () {
        Data._status = PlayerPrefs.GetInt("status");
        Data._username = PlayerPrefs.GetString("username");
        Data._password = PlayerPrefs.GetString("password");
        sliderImageId = 0;
        //DOTween.Init(true, true, LogBehaviour.Default);
        initCanvas();
    }

	// Update is called once per frame
	void Update () {
        if(!UIScore.active)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                IdUI_AR = 0;
                goBackToMainMenu();
            }  
        }
        if (canvasHome.activeSelf)
            checkSwipe();
    }

    private void goBackToMainMenu() {
        canvasHome.SetActive(true);
        canvasArMenu.SetActive(false);
        canvasEvents.SetActive(false);
        canvasInformation.SetActive(false);
        canvasSchedule.SetActive(false);
        canvasSplashScreen.SetActive(false);
        canvasLogIn.SetActive(false);
        canvasSignUp.SetActive(false);
        canvasSignUpOption.SetActive(false);
    }

    private void deactivateAllMenuCanvas(){
        canvasSplashScreen.SetActive(false);
        canvasLogIn.SetActive(false);
        canvasSignUp.SetActive(false);
        canvasSignUpOption.SetActive(false);
        canvasHome.SetActive(false);
        canvasInformation.SetActive(false);
        canvasEvents.SetActive(false);
        canvasSchedule.SetActive(false);
        canvasArMenu.SetActive(false);
    }

    private void initCanvas() {
        canvasSplashScreen.SetActive(true);
        canvasLogIn.SetActive(false);
        canvasSignUp.SetActive(false);
        canvasSignUpOption.SetActive(false);
        canvasHome.SetActive(false);
        canvasInformation.SetActive(false);
        canvasEvents.SetActive(false);
        canvasSchedule.SetActive(false);
        canvasArMenu.SetActive(false);
        Data._status = PlayerPrefs.GetInt("status");
        StartCoroutine(initSplashScreen());
    }

    private void initAr(){

        deactivateAllMenuCanvas();
    }

    #region database
    IEnumerator RegisterUser()
    {
        form = new WWWForm();
        form.AddField("username", inputSignUpName.text);
        form.AddField("birth", inputSignUpBirthday.text);
        form.AddField("email", inputSignUpEmail.text);
        form.AddField("phone", inputSignUpPhoneNumber.text);
        form.AddField("occupation", inputSignUpOcupation.text);
        form.AddField("password", inputSignUpPassword.text);
        form.AddField("ver_password", inputSignUpConfirmPassword.text);

        www = new WWW(url_register, form);
        yield return www;
        status = www.text;
        myUser = JsonUtility.FromJson<User>(status);
        Data._username = myUser.username;
        Data._password = myUser.password;
        Data._birth = myUser.birth;
        Data._email = myUser.email;
        Data._phone = myUser.phone;
        Data._occupation = myUser.occupation;
        Data._id = myUser.id;
    }

    IEnumerator LoginUser()
    {
        form = new WWWForm();
        form.AddField("username", Data._username);
        form.AddField("password", Data._password);
        www = new WWW(url_login, form);
        yield return www;
        status = www.text;
        myUser = JsonUtility.FromJson<User>(status);
        Data._username = myUser.username;
        Data._password = myUser.password;
        Data._birth = myUser.birth;
        Data._email = myUser.email;
        Data._phone = myUser.phone;
        Data._occupation = myUser.occupation;
        Data._id = myUser.id;
    }


    public static void playerSave()
    {
        PlayerPrefs.SetString("username", Data._username);
        PlayerPrefs.Save();
        PlayerPrefs.SetString("birth", Data._birth);
        PlayerPrefs.Save();
        PlayerPrefs.SetString("email", Data._email);
        PlayerPrefs.Save();
        PlayerPrefs.SetString("phone", Data._phone);
        PlayerPrefs.Save();
        PlayerPrefs.SetString("occoupation", Data._occupation);
        PlayerPrefs.Save();
        PlayerPrefs.SetString("password", Data._password);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("id", Data._id);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("status", Data._status);
        PlayerPrefs.Save();
    }
    #endregion

    #region Home Function
    private void initSignUp()
    {
        canvasSignUpOption.SetActive(true);
        canvasSplashScreen.SetActive(false);
    }
    private void initHomeMenu()
    {
        goBackToMainMenu();
    }
    private IEnumerator initSplashScreen() {
        yield return new WaitForSeconds(3f);
        Data._status = PlayerPrefs.GetInt("status");
        if (Data._status == ENUM_STATUS_LOGGED_IN){
            StartCoroutine(LoginUser());
            initHomeMenu();
        }
        else{
            initSignUp();
        }
    }

    private void checkSwipe() {
        if (!touchDetector.IsDraging)
            isSwiping = false;
        if (Input.touches.Length > 0 && Input.touches[0].position.y > 840)
        {
            if (touchDetector.SwipeRight && !isSwiping && !isSliding)
            {
                if (sliderImageId > 0)
                {
                    sliderImageId--;
                    slideImage(false);
                }
                isSwiping = true;
            }
            if (touchDetector.SwipeLeft && !isSwiping && !isSliding)
            {
                if (sliderImageId < listOfImgSlider.Count - 1)
                {
                    sliderImageId++;
                    slideImage(true);
                }
                isSwiping = true;
            }
        }
        //Debug.Log("Is Swiping " + isSwiping);
    }

    private void slideImage(bool _next) {
        string trace = "";
        Debug.Log("Slide " + _next);
        imgSliderCircle.sprite = listOfSliderCircleSprite[sliderImageId];
        isSliding = true;
        StartCoroutine(waitSlidingComplete());
        if (_next) {
            for (int i = 0; i < listOfImgSlider.Count; i++) {
                //listOfImgSlider[i].rectTransform.anchoredPosition = new Vector3(listOfImgSlider[i].rectTransform.anchoredPosition.x - 608, -234);
                listOfImgSlider[i].rectTransform.DOLocalMoveX(listOfImgSlider[i].rectTransform.anchoredPosition.x - 664, 0.9f, true);
                trace += listOfImgSlider[i].rectTransform.anchoredPosition.x.ToString() + " -> " + (listOfImgSlider[i].rectTransform.anchoredPosition.x - 608).ToString() + "\n";
            }
        }
        else {
            for (int i = 0; i < listOfImgSlider.Count; i++) {
                //listOfImgSlider[i].rectTransform.anchoredPosition = new Vector3(listOfImgSlider[i].rectTransform.anchoredPosition.x + 608, -234);
                listOfImgSlider[i].rectTransform.DOLocalMoveX(listOfImgSlider[i].rectTransform.anchoredPosition.x + 664, 0.9f, true);
                trace += listOfImgSlider[i].rectTransform.anchoredPosition.ToString() + " -> " + (listOfImgSlider[i].rectTransform.anchoredPosition.x + 608).ToString() + "\n";
            }
        }
        if (trace != "")
            Debug.Log(trace);
    }

    private IEnumerator waitSlidingComplete() {
        yield return new WaitForSeconds(1f);
        isSliding = false;
    }

    private bool checkSignUpField()
    {
        bool result = true;
        txtSignUpFeedback.text = "";
        for (int i = 0; i < listOfStarText.Count; i++){
            listOfStarText[i].gameObject.SetActive(false);
        }

        if (inputSignUpName.text == ""){
            listOfStarText[0].gameObject.SetActive(true);
            txtSignUpFeedback.text = "Must fill required field(s)";
            result = false;
        }
        if (inputSignUpBirthday.text == ""){
            listOfStarText[1].gameObject.SetActive(true);
            txtSignUpFeedback.text = "Must fill required field(s)";
            result = false;
        }
        if (inputSignUpEmail.text == "" && !inputSignUpEmail.text.Contains("@")){
            listOfStarText[2].gameObject.SetActive(true);
            txtSignUpFeedback.text = "Invalid Email";
            result = false;
        }
        if (inputSignUpPhoneNumber.text == ""){
            listOfStarText[3].gameObject.SetActive(true);
            txtSignUpFeedback.text = "Must fill required field(s)";
            result = false;
        }
        if (inputSignUpOcupation.text == ""){
            listOfStarText[4].gameObject.SetActive(true);
            txtSignUpFeedback.text = "Must fill required field(s)";
            result = false;
        }
        if (inputSignUpPassword.text == ""){
            listOfStarText[5].gameObject.SetActive(true);
            txtSignUpFeedback.text = "Must fill required field(s)";
            result = false;
        }
        if (inputSignUpConfirmPassword.text != inputSignUpPassword.text) {
            listOfStarText[6].gameObject.SetActive(true);
            txtSignUpFeedback.text = "Password didn't match";
            result = false;
        }
        return result;
    }
    #endregion
    
    #region Button Function
    public void onPressLoginButton() {
        //LOGIN
        canvasHome.SetActive(true);
        canvasLogIn.SetActive(false);
        isLoggedIn = true;
         
    }
    public void onPressSignUpGuestButton() {
        //LOGIN GUEST
        isGuest = true;
        isLoggedIn = true;
        PlayerPrefs.SetInt("status", 0);
        PlayerPrefs.Save();
        canvasHome.SetActive(true);
        canvasSignUpOption.SetActive(false);
    }
    public void onPressSignUpButton() {
        canvasSignUp.SetActive(true);
        canvasLogIn.SetActive(false);
    }
    public void onPressSignUpGoogleButton() {
        canvasSignUp.SetActive(true);
        canvasSignUpOption.SetActive(false);
    }
    public void onPressSignUpFacebookButton() {
        canvasSignUp.SetActive(true);
        canvasSignUpOption.SetActive(false);
    }
    public void onPressSignUpEmailButton() {
        canvasSignUp.SetActive(true);
        canvasSignUpOption.SetActive(false);
    }
    public void onPressSubmitSignUpButton() {
        if(checkSignUpField()) {
            StartCoroutine(RegisterUser());
            playerSave();
            PlayerPrefs.SetInt("status", 1);
            PlayerPrefs.Save();
            PlayerPrefs.SetString("username", inputSignUpName.text);
            PlayerPrefs.Save();
            PlayerPrefs.SetString("password", inputSignUpPassword.text);
            PlayerPrefs.Save();
            canvasHome.SetActive(true);
            canvasSignUp.SetActive(false);
        }
    }


    public void onPressEventsButton() {
        canvasEvents.SetActive(true);
        canvasHome.SetActive(false);
    }
    public void onPressScheduleButton() {
        canvasSchedule.SetActive(true);
        canvasHome.SetActive(false);
    }
    public void onPressArExperienceButton() {
        if (!isGuest){
            initAr();
            IdUI_AR = 1;
        }
    }
    public void onPressInformationButton() {
        canvasInformation.SetActive(true);
        canvasHome.SetActive(false);
    }
    public void onPressBackButton() {
        goBackToMainMenu();
    }
    public void onPressArMapButton() {
        initAr();
        IdUI_AR = 1;
    }
    public void onPressArEventInformationButton() {
        initAr();
        IdUI_AR = 1;
    }
    public void onPressTreasureHuntButton() {
        if(!isGuest)
        {
            initAr();
            IdUI_AR = 2;
        }

    }
    public void onPressStartTalkButton() {
        if (scheduleState == ScheduleState.Talks)
            changeScheduleState(ScheduleState.Closed);
        else
            changeScheduleState(ScheduleState.Talks);
    }
    public void onPressStartWorksButton() {
        if (scheduleState == ScheduleState.Works)
            changeScheduleState(ScheduleState.Closed);
        else
            changeScheduleState(ScheduleState.Works);
    }
    public void onPressStartMatchButton() {
        if (scheduleState == ScheduleState.Match)
            changeScheduleState(ScheduleState.Closed);
        else
            changeScheduleState(ScheduleState.Match);
    }
    #endregion
    private void changeScheduleState(ScheduleState _state) {
        scheduleState = _state;
        arrangeScheduleMenu();
    }

    private void arrangeScheduleMenu() {
        switch (scheduleState) {
            case ScheduleState.Closed:
                imgScheduleContent.sprite = listOfScheduleContentSprite[0];
                btnStartTalk.GetComponent<RectTransform>().anchoredPosition = new Vector3(-40f, -230f);
                btnStartWorks.GetComponent<RectTransform>().anchoredPosition = new Vector3(-40f, -329f);
                btnStartMatch.GetComponent<RectTransform>().anchoredPosition = new Vector3(-40f, -427f);
                break;
            case ScheduleState.Talks:
                imgScheduleContent.sprite = listOfScheduleContentSprite[1];
                btnStartTalk.GetComponent<RectTransform>().anchoredPosition = new Vector3(-40f, -230f);
                btnStartWorks.GetComponent<RectTransform>().anchoredPosition = new Vector3(-40f, -1124f);
                btnStartMatch.GetComponent<RectTransform>().anchoredPosition = new Vector3(-40f, -1219f);
                break;
            case ScheduleState.Works:
                imgScheduleContent.sprite = listOfScheduleContentSprite[2];
                btnStartTalk.GetComponent<RectTransform>().anchoredPosition = new Vector3(-40f, -230f);
                btnStartWorks.GetComponent<RectTransform>().anchoredPosition = new Vector3(-40f, -329f);
                btnStartMatch.GetComponent<RectTransform>().anchoredPosition = new Vector3(-40f, -803f);
                break;
            case ScheduleState.Match:
                imgScheduleContent.sprite = listOfScheduleContentSprite[3];
                btnStartTalk.GetComponent<RectTransform>().anchoredPosition = new Vector3(-40f, -230f);
                btnStartWorks.GetComponent<RectTransform>().anchoredPosition = new Vector3(-40f, -329f);
                btnStartMatch.GetComponent<RectTransform>().anchoredPosition = new Vector3(-40f, -427f);
                break;
            default:
                break;
        }
    }
}
