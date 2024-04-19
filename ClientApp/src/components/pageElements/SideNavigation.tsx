import {Link} from "react-router-dom";
import {useEffect, useRef, useState} from "react";

//Icons
import MenuIcon from '@mui/icons-material/Menu';
import HomeIcon from '@mui/icons-material/Home';
import PersonIcon from '@mui/icons-material/Person';
import RestaurantIcon from '@mui/icons-material/Restaurant';

const SideNavigation = () => {
    
    const [isNavOpen, setIsNavOpen] = useState(false);
    const navRef = useRef<HTMLDivElement | null>(null);
    
    const toggleNav = () => {
        setIsNavOpen(!isNavOpen);
    };

    const handleClickOutside = (event: MouseEvent) => {
        if (navRef.current && !navRef.current.contains(event.target as Node)) {
            setIsNavOpen(false);
        }
    };

    useEffect(() => {
        document.addEventListener("mousedown", handleClickOutside);
        return () => {
            document.removeEventListener("mousedown", handleClickOutside);
        };
    }, []);
    
    
    return (
        <>
            <nav ref={navRef} className={`sideNav ${!isNavOpen ? 'sideNavHidden' : ''}`}>
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