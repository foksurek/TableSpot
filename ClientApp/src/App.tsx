import React from "react";
import {BrowserRouter} from "react-router-dom";
import Routes from "./Routes.tsx";
import {AuthProvider} from "./contexts/AuthContext.tsx";
import Navigation from "./components/pageElements/Navigation.tsx";

const App : React.FC = () => {
  return (
    <BrowserRouter>
        <AuthProvider>
            <Navigation/>
            <Routes />
        </AuthProvider>
    </BrowserRouter>
  )
}

export default App
