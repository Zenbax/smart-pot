import axios from "axios";

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


const instance = axios.create({ //TODO: Man skal muligvis download axios: npm install axios@0.24.0
    baseURL: API_BASE_URL,
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
  });

export async function createUser (paramName, paramLastName, paramPassword, paramEmail,paramPhoneNumber){ //TODO: eventuelt parse til JSON et andet sted
        var jsonUserInfoDTO = JSON.stringify(
            {
                name: paramName,
                lastName: paramLastName,
                password: paramPassword,
                email: paramEmail,
                phoneNumber: paramPhoneNumber
            }
        )
        console.log(jsonUserInfoDTO)
    try{
        const response = await instance.post("/user/create", jsonUserInfoDTO);
        console.log(response);
        
    }
    catch{
        //TODO: ErrorHandling 
    }
    
}

export async function getPotFromId(id){
    try{
        const response = await instance.get("/pot/get/"+id)
        return response.data
    }
    catch{
        //TODO: ErrorHandling 
    }
    

    
}

export async function getAllPots(){
    return potArray;
    /*
    try{
        const response = await instance.get("/pot/get/all")
        return response.data
    }
    catch{
        //TODO: ErrorHandling 
    }*/
    
}


export async function loginUser(username, password) {
    const userInfoDTO ={
        Username: username,
        Password: password
    }
    var jsonUserInfoDTO
    JSON.stringify(userInfoDTO, jsonUserInfoDTO)
    try{
       response = instance.post("/Auth/login", jsonUserInfoDTO)
       return response
    }
    catch{
        //TODO: ErrorHandling 
    }
    
  }