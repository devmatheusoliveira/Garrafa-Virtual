using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
public class layout : MonoBehaviour
{
    public Button buttonCadastrar, buttonTelaDeLogin, buttonLogar, ButtonTrocar;

    public InputField inputLogin, inputSenha;

    public Canvas[] ListaDeTelas;

    public Canvas ProcurarQRcode;

    public int TelaAtual = 0;

    public float temporizador;
    public ClasseDados[] dados;

    public Text final;

    void Start()
    {
        buttonCadastrar.onClick.AddListener(cadastar);
        buttonTelaDeLogin.onClick.AddListener(telaDeLogin);
        buttonLogar.onClick.AddListener(logar);
        dados[0].ButtonMais.onClick.AddListener(GarrafaAdd1);
        dados[1].ButtonMais.onClick.AddListener(GarrafaAdd2);
        dados[0].ButtonMenos.onClick.AddListener(GarrafaTira1);
        dados[1].ButtonMenos.onClick.AddListener(GarrafaTira2);
        ButtonTrocar.onClick.AddListener(ScanAndTrocar);
        
    }


    void Update()
    {
        if(temporizador >= 0.5f){
            for(int i = 0; i < ListaDeTelas.Length; i++){
                ListaDeTelas[i].enabled = false;
                if(TelaAtual==i){
                    ListaDeTelas[TelaAtual].enabled = true;
                }
            }
            temporizador = 0;
        }else{
            temporizador += Time.deltaTime; 
        }

        final.text = "Parabéns você evitou que " + (dados[0].quantidadeDeGarrafas + dados[1].quantidadeDeGarrafas) + " garrafas fossem para o meio ambiente e em troca, iremos te Premiar com alguns descontos!";
        QrCode();
        

    }
    void ScanAndTrocar(){
        TelaAtual += 1;
    }
    void QrCode(){
        if(TelaAtual == 3){
            if(isTrackingMarker("ImageTarget")){
                TelaAtual += 1;
                ProcurarQRcode.enabled = false;
            }else{
                ProcurarQRcode.enabled = true;
            }   
        }
    }

    void GarrafaAdd1(){
        dados[0].quantidadeDeGarrafas += 1;
        dados[0].Produto.text = dados[0].quantidadeDeGarrafas.ToString();
    }
    void GarrafaAdd2(){
        dados[1].quantidadeDeGarrafas += 1;
        dados[1].Produto.text = dados[1].quantidadeDeGarrafas.ToString();
    }
    void GarrafaTira1(){
        dados[0].quantidadeDeGarrafas -= 1;
        dados[0].Produto.text = dados[0].quantidadeDeGarrafas.ToString();
    }
    void GarrafaTira2(){
        dados[1].quantidadeDeGarrafas += 1;
        dados[1].Produto.text = dados[1].quantidadeDeGarrafas.ToString();
    }

    void telaDeLogin(){
        TelaAtual = 1;
    }
    void logar(){
        string login = "Adimin";
        string senha = "adimin";
        print("login: "+ inputLogin.text);
        print("senha: "+ inputSenha.text);
        if((inputLogin.text == login)&&(inputSenha.text == senha)){
            TelaAtual += 1;
        }else{
            print("nop");
        }
        
    }
    void cadastar()
    {
        print("cadastra");
    }

    private bool isTrackingMarker(string imageTargetName)
    {
        var imageTarget = GameObject.Find(imageTargetName);
        var trackable = imageTarget.GetComponent<TrackableBehaviour>();
        var status = trackable.CurrentStatus;
        return status == TrackableBehaviour.Status.TRACKED;
    }

}
