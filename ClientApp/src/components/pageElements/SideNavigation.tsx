import {Link} from "react-router-dom";

//Icons
import HomeIcon from '@mui/icons-material/Home';
import PersonIcon from '@mui/icons-material/Person';
import RestaurantIcon from '@mui/icons-material/Restaurant';

const SideNavigation = () => {
    return (
        <>
            <nav className="sideNav">
                <div className="sideNavHeader">
                    <h2>Dashboard</h2>
                </div>
                <ul className="sideNavList">
                    <li><HomeIcon/><Link to="/">Home</Link></li>
                    <li><PersonIcon/><Link to="/">My Restaurants</Link></li>
                    <li><RestaurantIcon/><Link to="/">Employees</Link></li>
                    <li><Link to="/">Other Example Option</Link></li>
                    <li><Link to="/">Other Example Option</Link></li>
                    <li><Link to="/">Other Example Option</Link></li>
                    <li><Link to="/">Other Example Option</Link></li>
                </ul>
            </nav>
        </>
    )
}

export default SideNavigation 