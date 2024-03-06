import React from "react";
import {BrowserRouter} from "react-router-dom";
import Routes from "./Routes.tsx";
import {AuthProvider} from "./contexts/AuthContext.tsx";

const App : React.FC = () => {
  return (
    <BrowserRouter>
        <AuthProvider>
            <Routes />
        </AuthProvider>
    </BrowserRouter>
  )
}

export default App
