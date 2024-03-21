import {Button} from "@mui/material";
import {Link} from "react-router-dom";
import SearchBar from "../SearchBar.tsx";

const Navigation = () => {
    
    if (document.body.scrollWidth < 1700)
        return (
            <nav className="nav-mobile">
                <h1>TableSpot</h1>
                <Button variant="outlined">Log In</Button>
            </nav>
        )
        
    return (
        <nav className="nav-full">
            <h1>TableSpot</h1>
            <ul>
                <li><Link to="">Home</Link></li>
                <li>Browse</li>
                <li>Third option</li>
            </ul>
            <SearchBar/>
            <span>
                <Button variant="outlined">Log In</Button>
                <Button variant="contained">Add your business</Button>
            </span>
        </nav>
    )
}

export default Navigation