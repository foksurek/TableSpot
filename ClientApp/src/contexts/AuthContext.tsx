import React, {createContext, useContext, useEffect, useState} from 'react';
import axios from 'axios';
import API_URLS from "../ApiConst/ApiUrls.ts";

type UserProps = {
    id: number;
    email: string;
}

type ApiResponse = {
    code: number;
    data: {
        email: string;
        accountId: string;
        accountType: {
            id: string;
            type: string;
        }
    }
}

interface AuthContextType {
    user: UserProps | null;
    setUser: React.Dispatch<React.SetStateAction<UserProps | null>>;
}

const AuthContext = createContext<AuthContextType>({
    user: null,
    setUser: () => {},
});

type Props = {
    children: React.ReactNode;
};

export const AuthProvider = (props: Props) => {
    const [user, setUser] = useState<{ id: number; email: string } | null>(null);

    useEffect(() => {
        const fetchData = async () => {
            let resp = await axios.get<ApiResponse>(API_URLS.ACCOUNT.GET_ACCOUNT_DATA, {withCredentials: true});
            let data = resp.data;
            setUser({
                id: parseInt(data.data.accountId),
                email: data.data.email
            });
        }
        fetchData();
    }, []);
    
    
    
    return (
        <AuthContext.Provider value={{ user, setUser }}>
            {props.children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => useContext(AuthContext);
