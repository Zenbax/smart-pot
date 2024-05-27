import Plant404 from '../images/Plant404.jpg'
import {Container, Row, Col} from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import {Link, useNavigate } from 'react-router-dom';
import "../Styling/PageNotFound.css"

const PageNotFound = () => {
    const navigate = useNavigate();
    const handleClick = () => {
        navigate("/smart-pot")
      };
return(
    <Container>
    <Row>
        <Col>
        <img id='plant404' src={Plant404}/>
        </Col>
        <Col>
        <div id='PageNotFoundText'>
            <h1>404</h1>
            <h2>Page Not Found</h2>
            <Link to="/smart-pot"><button type="button" onClick={handleClick}> Home</button></Link>
        </div>
        
        </Col>
    </Row>
</Container>
)
}
export default PageNotFound 
