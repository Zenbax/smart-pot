import { useEffect, useState } from "react";
import { useParams } from "react-router-dom"
import { getPotFromId } from "../API/API_config";
export default function PotDetails(){
    const {potID} = useParams();
    const [pot, setPot] = useState();

  useEffect(() => {
    const fetchData = async () => {
        const potData = await getPotFromId(potID);
        setPot(potData);
    };
    fetchData();
    console.log(pot);
}, [])



    return(
        
        <h1>{pot?.NameOfpot}</h1>
    );
}
