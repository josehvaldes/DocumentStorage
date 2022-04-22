import { atom } from 'recoil';

const documentsByUserAtom = atom({
    key: 'documentsByUser',
    default: null
});

export { documentsByUserAtom };