import { InteractionRequiredAuthError, InteractionStatus } from "@azure/msal-browser";
import { useMsal } from "@azure/msal-react";
import { useEffect } from "react";
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
                setAuth({ "token": token, account: accounts[0], "name":"default1" });
            }).catch((error) => {
                if (error instanceof InteractionRequiredAuthError) {
                    instance.acquireTokenPopup(accessTokenRequest).then(function (accessTokenResponse) {
                        // Acquire token interactive success
                        var token = accessTokenResponse.accessToken;
                        // Call your API with token
                        setAuth({ "token": token, account: accounts[0] });

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

