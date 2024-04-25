import { Skeleton } from "@mui/material";
import SideNavigation from "components/pageElements/SideNavigation.tsx";

const DashboardPage = () => {
    
    return (
        <>
            <SideNavigation/>
            <main className="dashboardPage">
                <div className="box"><Skeleton variant="rounded" height={'100%'}/></div>
                <div className="box"><Skeleton variant="rounded" height={'100%'}/></div>
                <div className="box"><Skeleton variant="rounded" height={'100%'}/></div>
                <div className="box"><Skeleton variant="rounded" height={'100%'}/></div>
                <div className="box"><Skeleton variant="rounded" height={'100%'}/></div>
            </main>
        </>
    )
    
}

export default DashboardPage