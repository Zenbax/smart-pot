import Smartpot from '../Components/SmartPot';
import '../Styling/PotsList.css'

const PotsList = ({ pots }) => {
    return (
        <div id='list'>
            {pots.map((e) => {
                return <Smartpot pot={e} />;
            })}
        </div>
    );
};


export default PotsList
