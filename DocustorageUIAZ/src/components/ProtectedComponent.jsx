import { InteractionRequiredAuthError, InteractionStatus } from "@azure/msal-browser";
import { AuthenticatedTemplate, useMsal } from "@azure/msal-react";
import React, { useEffect, useState } from "react";
import { useRecoilState } from 'recoil';
import { authAtom } from '../_state';
import { webApiAccessRequest } from "../authConfig";

export { ProtectedComponent };

function ProtectedComponent() {
    const { instance, inProgress, accounts } = useMsal();
    const [auth, setAuth] = useRecoilState(authAtom);

    useEffect(() => {
        if (!auth && inProgress === InteractionStatus.None) {
            const accessTokenRequest = {
                ...webApiAccessRequest,
                account: accounts[0]
            }
            instance.acquireTokenSilent(accessTokenRequest).then((accessTokenResponse) => {
                // Acquire token silent success
                var token = accessTokenResponse.accessToken;
                setAuth({ "token": token });
            }).catch((error) => {
                if (error instanceof InteractionRequiredAuthError) {
                    instance.acquireTokenPopup(accessTokenRequest).then(function (accessTokenResponse) {
                        // Acquire token interactive success
                        var token = accessTokenResponse.accessToken;
                        // Call your API with token
                        setAuth({ "token": token });

                    }).catch(function (error) {
                        // Acquire token interactive failure
                        console.log(error);
                    });
                }
                console.log(error);
            })
        }
    }, [instance, accounts, inProgress, auth]);

    return null;
}

