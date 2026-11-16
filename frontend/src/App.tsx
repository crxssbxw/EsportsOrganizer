import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import TeamsPage from "./pages/TeamsPage"
import MatchesPage from "./pages/MatchesPage"
import EventsPage from "./pages/EventsPage"
import NavBar from "./components/NavBar";

function App(){
 return(
  <BrowserRouter>
    <NavBar/>
    <Routes>
      <Route path="/teams" element={<TeamsPage/>}/>
      <Route path="/matches" element={<MatchesPage/>}/>
      <Route path="/events" element={<EventsPage/>}/>
      <Route path="/" element={<Navigate to="/teams" replace />} />
    </Routes>
  </BrowserRouter>
 )
}

export default App;