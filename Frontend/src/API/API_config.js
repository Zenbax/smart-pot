import axios from "axios";
import Cookies from 'js-cookie';

const API_BASE_URL = "http://13.53.174.85/";

var potArray=[
    {
        nameOfPot: "Soveværelses vindue",
        id: 21938,
        plant: {
            nameOfPlant: "Monstera",
            JordMinimumsFugtighed: 35,
            imageUrl: "https://static.vecteezy.com/system/resources/previews/003/193/486/original/cute-cartoon-home-plant-in-clay-pot-illustration-vector.jpg"
        }, 
        JordFugtighedProcent: 55,
        VandBeholderProcent: 50,
        VandingsLog: [
            {
                VandingsTidspunkt: new Date(2006, 0, 2, 15, 4, 5),
                mændeML: 100,
            },
            {
                VandingsTidspunkt: new Date(2006, 0, 2, 15, 4, 5),
                mændeML: 100,
            },
            {
                VandingsTidspunkt: new Date(2006, 0, 2, 15, 4, 5),
                mændeML: 100,
            },
            {
                VandingsTidspunkt: new Date(2006, 0, 2, 15, 4, 5),
                mændeML: 100,
            },
            {
                VandingsTidspunkt: new Date(2006, 0, 2, 15, 4, 5),
                mændeML: 100,
            }
        ],
        Fugtighedslog: [
            {
                TimeStamp: new Date(2006, 0, 2, 15, 4, 5),
                fugtighedProcent: 45
            },
            {
                TimeStamp: new Date(2006, 0, 2, 15, 4, 5),
                fugtighedProcent: 55
            },
            {
                TimeStamp: new Date(2006, 0, 2, 15, 4, 5),
                fugtighedProcent: 33
            },
            {
                TimeStamp: new Date(2006, 0, 2, 15, 4, 5),
                fugtighedProcent: 70
            },
            {
                TimeStamp: new Date(2006, 0, 2, 15, 4, 5),
                fugtighedProcent: 55
            },
            {
                TimeStamp: new Date(2006, 0, 2, 15, 4, 5),
                fugtighedProcent: 50
            },
        ],
    },
    {
        nameOfPot: "Stue vindue #1",
        id: 87362,
        plant: {
            nameOfPlant: "Philodendron",
            JordMinimumsFugtighed: 35,
            imageUrl: "https://static.vecteezy.com/system/resources/previews/003/193/486/original/cute-cartoon-home-plant-in-clay-pot-illustration-vector.jpg"
        }, 
        JordFugtighedProcent: 55,
        VandBeholderProcent: 50,
        VandingsLog: [
            {
                VandingsTidspunkt: new Date(2006, 0, 2, 15, 4, 5),
                mændeML: 100,
            },
            {
                VandingsTidspunkt: new Date(2006, 0, 2, 15, 4, 5),
                mændeML: 100,
            },
            {
                VandingsTidspunkt: new Date(2006, 0, 2, 15, 4, 5),
                mændeML: 100,
            },
            {
                VandingsTidspunkt: new Date(2006, 0, 2, 15, 4, 5),
                mændeML: 100,
            },
            {
                VandingsTidspunkt: new Date(2006, 0, 2, 15, 4, 5),
                mændeML: 100,
            }
        ],
        Fugtighedslog: [
            {
                TimeStamp: new Date(2006, 0, 2, 15, 4, 5),
                fugtighedProcent: 45
            },
            {
                TimeStamp: new Date(2006, 0, 2, 15, 4, 5),
                fugtighedProcent: 55
            },
            {
                TimeStamp: new Date(2006, 0, 2, 15, 4, 5),
                fugtighedProcent: 33
            },
            {
                TimeStamp: new Date(2006, 0, 2, 15, 4, 5),
                fugtighedProcent: 70
            },
            {
                TimeStamp: new Date(2006, 0, 2, 15, 4, 5),
                fugtighedProcent: 55
            },
            {
                TimeStamp: new Date(2006, 0, 2, 15, 4, 5),
                fugtighedProcent: 50
            },
        ],
        VandingsInstillinger: {
            JordMinimumsFugtighed: 35,
            vandingsMængdeML: 100
        }
    },
    {
        nameOfPot: "Stue vindue #2",
        id: 73837,
        plant: {
            nameOfPlant: "Lilje",
            JordMinimumsFugtighed: 35,
            imageUrl: "https://static.vecteezy.com/system/resources/previews/003/193/486/original/cute-cartoon-home-plant-in-clay-pot-illustration-vector.jpg"

        }, 
        JordFugtighedProcent: 55,
        VandBeholderProcent: 50,
        VandingsLog: [
            {
                VandingsTidspunkt: new Date(2006, 0, 2, 15, 4, 5),
                mændeML: 100,
            },
            {
                VandingsTidspunkt: new Date(2006, 0, 2, 15, 4, 5),
                mændeML: 100,
            },
            {
                VandingsTidspunkt: new Date(2006, 0, 2, 15, 4, 5),
                mændeML: 100,
            },
            {
                VandingsTidspunkt: new Date(2006, 0, 2, 15, 4, 5),
                mændeML: 100,
            },
            {
                VandingsTidspunkt: new Date(2006, 0, 2, 15, 4, 5),
                mændeML: 100,
            }
        ],
        Fugtighedslog: [
            {
                TimeStamp: new Date(2024, 3, 2, 15, 4, 5),
                fugtighedProcent: 45
            },
            {
                TimeStamp: new Date(2024, 3, 2, 15, 4, 5),
                fugtighedProcent: 55
            },
            {
                TimeStamp: new Date(2024, 3, 2, 15, 4, 5),
                fugtighedProcent: 33
            },
            {
                TimeStamp: new Date(2024, 3, 2, 15, 4, 5),
                fugtighedProcent: 70
            },
            {
                TimeStamp: new Date(2024, 3, 2, 15, 4, 5),
                fugtighedProcent: 55
            },
            {
                TimeStamp: new Date(2006, 3, 2, 15, 4, 5),
                fugtighedProcent: 50
            },
        ],
    }
]



const instance = axios.create({
    baseURL: API_BASE_URL,
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
      //Bearer: Cookies.get('token')
      Authorization: 'Bearer '+ localStorage.getItem('token')
    },
  });

export async function createUser (paramName, paramLastName, paramPassword, paramEmail,paramPhoneNumber){ //TODO: eventuelt parse til JSON et andet sted
        var jsonUserInfoDTO = JSON.stringify(
            {
                name: paramName,
                lastName: paramLastName,
                email: paramEmail,
                password: paramPassword,
                phoneNumber: paramPhoneNumber
            }
        )
        console.log(jsonUserInfoDTO)
    try{
        const response = await instance.post("/auth/register", jsonUserInfoDTO,);
        console.log(response)
        
    }
    catch(Error){
        console.log(Error.message)
    }
    
}

export async function getPotFromId(id){
    try{
        const response = await instance.get("/pot/get/"+id)
        console.log(response)
        return response.data
    }
    catch(error){
        if(error?.response.status === 401){
            notAuthorized()
        }
    }
    

    
}

export async function getAllPots(){
    try{
        console.log("here")
        const response = await instance.get("/pot/get/all").catch((error) => {
            if(error.response.status ===401){

            }
          })
        console.log(response)
        return response.data.pots
    }
    catch(error){
        if(error?.response.status === 401){
            notAuthorized()
        }
    }
    
}


export async function loginUser(email, password) {
    var jsonUserInfoDTO = JSON.stringify(
        {
        email: email,
        Password: password
        }
    )
    console.log(jsonUserInfoDTO)


    try{
        const response = await instance.post("/auth/login", jsonUserInfoDTO);
        
        console.log(response);  
       
       //Cookies.set('token', response.token, { expires: 7, secure: true });
       localStorage.setItem('token', response.data.token);
       instance.defaults.headers.common['Authorization'] ='Bearer '+ response.data.token;
       return true
    }
    catch(error){
        if(error?.response.message === 400){
            return "Invalid Credentials"
        }
    }
    
  }

  function notAuthorized(){
    localStorage.setItem('token', "")
  }