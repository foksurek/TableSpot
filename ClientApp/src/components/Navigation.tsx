import {Button} from "@mui/material";

const Navigation = () => {
    return (
        <nav>
            <h1>TableSpot</h1>
            <ul>
                <li>Home</li>
                <li>Browse</li>
                <li>Third option</li>
            </ul>
            <span>
                <Button variant="outlined">Log In</Button>
                <Button variant="contained">Add your business</Button>
            </span>
        </nav>
    )
}

export default Navigation