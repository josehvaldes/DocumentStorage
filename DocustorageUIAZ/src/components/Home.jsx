import { useIsAuthenticated } from "@azure/msal-react";
import { useMsal } from "@azure/msal-react";
import React, { useEffect, useState } from "react";
import { ProfileContent } from "./ProfileContent";

export { Home };

function Home() {

    const isAuthenticated = useIsAuthenticated();
    const { instance, accounts } = useMsal();

    const name = accounts[0] && accounts[0].name;

    useEffect(() => {
    }, []);

    return (
        <div className="">
            <h5 className="card-title">
                <center>
                    You are awesome, {name}
                    <br /><br />
                    <ProfileContent />
                </center>
            </h5>
            <br />
        </div>
    );
}
