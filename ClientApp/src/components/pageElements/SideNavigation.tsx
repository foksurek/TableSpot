import {Link} from "react-router-dom";
import {useState} from "react";

//Icons
import MenuIcon from '@mui/icons-material/Menu';
import HomeIcon from '@mui/icons-material/Home';
import PersonIcon from '@mui/icons-material/Person';
import RestaurantIcon from '@mui/icons-material/Restaurant';

const SideNavigation = () => {
    
    const [isNavOpen, setIsNavOpen] = useState(false);

    const toggleNav = () => {
        setIsNavOpen(!isNavOpen);
    };
    
    return (
        <>
            <nav className={`sideNav ${!isNavOpen ? 'sideNavHidden' : ''}`}>
                <div className="sideNavHeader">
                    <h2>Dashboard</h2>
                    <MenuIcon onClick={toggleNav}/>
                </div>
                <ul className="sideNavList">
                    <li><HomeIcon/><Link to="/dashboard">Home</Link></li>
                    <li><PersonIcon/><Link to="/dashboard/restaurants">My Restaurants</Link></li>
                    <li><RestaurantIcon/><Link to="/dashboard/employees">Employees</Link></li>
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