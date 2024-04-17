import axios from "axios";

const API_BASE_URL = "https://localhost:7216"; //TODO: Nok en anden ip

const instance = axios.create({ //TODO: Man skal muligvis download axios: npm install axios@0.24.0
    baseURL: API_BASE_URL,
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
  });

export async function createUser (username, password){ //TODO: eventuelt parse til JSON et andet sted
        var jsonUserInfoDTO = JSON.stringify(
            {
                Username: username,
                Password: password,
                Email: 'dummy@email.com',
                Number: 83473984
            }
        )
        console.log(jsonUserInfoDTO);
    /*try{
        const response = await instance.post("/users", jsonUserInfoDTO)
        return response
    }
    catch{
        //TODO: ErrorHandling 
    }*/
    
}



export async function getAllPots(){
    return potArray=[
        {
            NameOfpot: "Soveværelses vindue",
            Plante: {
                NavnPåPlante: "Monstera",
                JordMinimumsFugtighed: 35,
                BilledeURL: "https://static.vecteezy.com/system/resources/previews/003/193/486/original/cute-cartoon-home-plant-in-clay-pot-illustration-vector.jpg"
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
            NameOfpot: "Stue vindue #1",
            Plante: {
                NavnPåPlante: "Philodendron",
                JordMinimumsFugtighed: 35,
                BilledeURL: "https://static.vecteezy.com/system/resources/previews/003/193/486/original/cute-cartoon-home-plant-in-clay-pot-illustration-vector.jpg"
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
            NameOfpot: "Stue vindue #2",
            Plante: {
                NavnPåPlante: "Lilje",
                JordMinimumsFugtighed: 35,
                BilledeURL: "https://static.vecteezy.com/system/resources/previews/003/193/486/original/cute-cartoon-home-plant-in-clay-pot-illustration-vector.jpg"

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
        }
    ]
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