import SideNavigation from "../../components/pageElements/SideNavigation.tsx";

const restaurantsPage = () => {
    return (
        <>
            <SideNavigation/>
            <main className="DashboardRestaurantsContainer">
                <div className="header">
                    <h1>My Businesses</h1>
                    <div>Add new</div>
                </div>
                <div className="content">
                    
                </div>
            </main>
        </>
    )
}
export default restaurantsPage;