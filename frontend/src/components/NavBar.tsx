import { Link, NavLink } from "react-router-dom";
import "./NavBar.css"

function NavBar(){
    return(
        <nav className="nav-block">
            <NavLink className="nav-link" to="/teams">Команды</NavLink>
            <NavLink className="nav-link" to="/matches">Матчи</NavLink>
            <NavLink className="nav-link" to="/events">События</NavLink>
        </nav>
    )
}

export default NavBar;