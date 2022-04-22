import { atom } from 'recoil';

const documentsByGroupAtom = atom({
    key: 'documentsByGroup',
    default: null
});

export { documentsByGroupAtom };