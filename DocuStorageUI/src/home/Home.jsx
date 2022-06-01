import { useEffect } from 'react';
import { useRecoilValue } from 'recoil';

import { authAtom, usersAtom } from '_state';
import { useUserActions } from '_actions';

export { Home };

function Home() {
    const auth = useRecoilValue(authAtom);
    const users = useRecoilValue(usersAtom);
    const userActions = useUserActions();

    useEffect(() => {
        userActions.getAll();
    }, []);

    return (
        <div className="containerBody col-md-6 offset-md-3">
            <center>
                <h2>Welcome {auth?.username}</h2>
            </center>
            
        </div>
    );
}
