import { atom } from 'recoil';

const categoriesAtom = atom({
    key: 'categories',
    default: null,
    writable: true
});

export { categoriesAtom };