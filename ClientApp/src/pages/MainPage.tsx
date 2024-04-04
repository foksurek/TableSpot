const MainPage = () => {

    return(
        <>
            <main className="pageContainer">
                <section id="recentlySearched">
                    {/*TODO: Think about how good is it*/}
                    <h2>Recently searched</h2>
                    {localStorage.getItem("restaurants") ?
                            (JSON.parse(localStorage.getItem("restaurants") as unknown as string).map((restaurant: string) => {
                                return (
                                    <div key={restaurant}>
                                        <a href={`/restaurant/${restaurant}`}>{restaurant}</a>
                                    </div>
                                )
                            }))
                            :
                            (<span>Nothing</span>)
                    }
                </section>
            </main>
        </>
    )
}

export default MainPage
